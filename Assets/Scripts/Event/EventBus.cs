using System;
using System.Collections.Generic;
using System.Linq;
using Enum;

namespace Event
{
    public delegate void EventHandler<T>(ref T e) where T : struct, IEvent;

    public class EventBus
    {
        private EventBus(){}

        public static EventBus Instance { get; } = new EventBus();

        private Dictionary<int, SortedDictionary<int, List<Delegate>>> _handlers = new();

        public void Register<T>(EventPriority priority, EventHandler<T> handler) where T : struct, IEvent
        {
            var id = new T().Id;

            if (!_handlers.TryGetValue(id, out var priorityDict))
            {
                priorityDict = new();
                _handlers[id] = priorityDict;
            }

            if (!priorityDict.TryGetValue((int) priority, out var list))
            {
                list = new();
                priorityDict[(int)priority] = list;
            }

            list.Add(handler);
        }

        /// <summary>
        /// 当没有实现阻断接口，就使用这个来发送事件，一般不使用这个
        /// </summary>
        /// <param name="e"></param>
        /// <typeparam name="T"></typeparam>
        private void Post<T>(ref T e) where T : struct, IEvent
        {
            if (!_handlers.TryGetValue(e.Id, out var priorityDict))
                return;

            foreach (var level in priorityDict.OrderBy(p => p.Key))
            {
                foreach (var handler in level.Value)
                {
                    if (handler is EventHandler<T> typedHandler)
                    {
                        typedHandler.Invoke(ref e);

                        if (e is ICancelable {IsCancelled: true})
                            return;
                    }
                }
            }
        }

        /// <summary>
        /// 当实现了阻断接口，就使用这个来发送事件
        /// </summary>
        /// <param name="e"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回true就应该继续进行，false就是被阻断了不应该继续进行</returns>
        public bool TryPost<T>(ref T e) where T : struct, IEvent
        {
            Post(ref e);
            if (e is IRejectable {IsRejected: true} rejectable)
            {
                return !rejectable.IsRejected;
            }
            // 没有实现Reject就继续执行
            return true;
        }
    }
}