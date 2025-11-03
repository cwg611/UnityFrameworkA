using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using ILRuntime.Runtime.Intepreter;
using JEngine.UI;
using LitJson;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using WebSocketSharp;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;
using Object = System.Object;

namespace JEngine.Helper
{
    public class RegisterDelegateConvertorHelper
    {
        private static RegisterDelegateConvertorHelper Instance;

        public static void HelperRegister(AppDomain appdomain)
        {
            if (Instance == null)
            {
                Instance = new RegisterDelegateConvertorHelper();
            }
            Instance.Register(appdomain);
        }

        public void Register(AppDomain appdomain)
        {
            appdomain.DelegateManager.RegisterDelegateConvertor<JEngine.Core.BindableProperty<System.Int64>.onChange>((act) =>
            {
                return new JEngine.Core.BindableProperty<System.Int64>.onChange((val) =>
                {
                    ((Action<System.Int64>)act)(val);
                });
            });


            appdomain.DelegateManager.RegisterDelegateConvertor<JEngine.Core.BindableProperty<System.Object>.onChange>((act) =>
            {
                return new JEngine.Core.BindableProperty<System.Object>.onChange((val) =>
                {
                    ((Action<System.Object>)act)(val);
                });
            });


            appdomain.DelegateManager.RegisterDelegateConvertor<Predicate<String>>(act =>
            {
                return new Predicate<String>(obj =>
                {
                    return ((Func<String, Boolean>)act)(obj);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<ParameterizedThreadStart>(act =>
            {
                return new ParameterizedThreadStart(obj =>
                {
                    ((Action<Object>)act)(obj);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<EventHandler<MessageEventArgs>>(act =>
            {
                return new EventHandler<MessageEventArgs>((sender, e) =>
                {
                    ((Action<Object, MessageEventArgs>)act)(sender, e);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<String>>(act =>
            {
                return new UnityAction<String>(arg0 =>
                {
                    ((Action<String>) act)(arg0);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<Boolean>>(act =>
            {
                return new UnityAction<Boolean>(arg0 =>
                {
                    ((Action<Boolean>) act)(arg0);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<WaitCallback>(act =>
            {
                return new WaitCallback(state => { ((Action<Object>) act)(state); });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction>(act =>
            {
                return new UnityAction(() => { ((Action) act)(); });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<Single>>(act =>
            {
                return new UnityAction<Single>(arg0 => { ((Action<Single>) act)(arg0); });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<UnhandledExceptionEventHandler>(act =>
            {
                return new UnhandledExceptionEventHandler((sender, e) =>
                {
                    ((Action<Object, UnhandledExceptionEventArgs>) act)(sender, e);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<Predicate<UnityEngine.Object>>(act =>
            {
                return new Predicate<UnityEngine.Object>(obj => { return ((Func<UnityEngine.Object, Boolean>) act)(obj); });
            });
            appdomain.DelegateManager
                .RegisterDelegateConvertor<Predicate<ILTypeInstance>>(act =>
                {
                    return new Predicate<ILTypeInstance>(obj =>
                    {
                        return ((Func<ILTypeInstance, Boolean>) act)(obj);
                    });
                });
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<Int32>>(act =>
            {
                return new UnityAction<Int32>(arg0 => { ((Action<Int32>) act)(arg0); });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<Action<JsonData>>(action =>
            {
                return new Action<JsonData>(a => { ((Action<JsonData>) action)(a); });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction>(act =>
            {
                return new UnityAction(async () => { ((Action) act)(); });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<ThreadStart>(act =>
            {
                return new ThreadStart(() => { ((Action) act)(); });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<Predicate<CoroutineAdapter.Adaptor>>(
                act =>
                {
                    return new Predicate<CoroutineAdapter.Adaptor>(obj =>
                    {
                        return ((Func<CoroutineAdapter.Adaptor, Boolean>) act)(obj);
                    });
                });
            appdomain.DelegateManager.RegisterDelegateConvertor<Predicate<MonoBehaviourAdapter.Adaptor>>(act =>
            {
                return new Predicate<MonoBehaviourAdapter.Adaptor>(obj =>
                {
                    return ((Func<MonoBehaviourAdapter.Adaptor, Boolean>)act)(obj);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<ElapsedEventHandler>(act =>
            {
                return new ElapsedEventHandler((sender, e) =>
                {
                    ((Action<Object, ElapsedEventArgs>)act)(sender, e);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<UIEventHandle<PointerEventData>>(act =>
            {
                return new UIEventHandle<PointerEventData>((go, eventData) =>
                {
                    ((Action<GameObject, PointerEventData>)act)(go, eventData);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<Predicate<KeyValuePair<String, ILTypeInstance>>>(act =>
            {
                return new Predicate<KeyValuePair<String, ILTypeInstance>>(obj =>
                {
                    return ((Func<KeyValuePair<String, ILTypeInstance>, Boolean>)act)(obj);
                });
            });


            //Add
            appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketOpenDelegate>((act) =>
            {
                return new BestHTTP.WebSocket.OnWebSocketOpenDelegate((webSocket) =>
                {
                    ((Action<BestHTTP.WebSocket.WebSocket>)act)(webSocket);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketMessageDelegate>((act) =>
            {
                return new BestHTTP.WebSocket.OnWebSocketMessageDelegate((webSocket, message) =>
                {
                    ((Action<BestHTTP.WebSocket.WebSocket, System.String>)act)(webSocket, message);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketBinaryDelegate>((act) =>
            {
                return new BestHTTP.WebSocket.OnWebSocketBinaryDelegate((webSocket, data) =>
                {
                    ((Action<BestHTTP.WebSocket.WebSocket, System.Byte[]>)act)(webSocket, data);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketClosedDelegate>((act) =>
            {
                return new BestHTTP.WebSocket.OnWebSocketClosedDelegate((webSocket, code, message) =>
                {
                    ((Action<BestHTTP.WebSocket.WebSocket, System.UInt16, System.String>)act)(webSocket, code, message);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketErrorDelegate>((act) =>
            {
                return new BestHTTP.WebSocket.OnWebSocketErrorDelegate((webSocket, reason) =>
                {
                    ((Action<BestHTTP.WebSocket.WebSocket, System.String>)act)(webSocket, reason);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<JEngine.Core.BindableProperty<System.Int32>.onChange>((act) =>
            {
                return new JEngine.Core.BindableProperty<System.Int32>.onChange((val) =>
                {
                    ((Action<System.Int32>)act)(val);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<JEngine.Core.BindableProperty<System.String>.onChange>((act) =>
            {
                return new JEngine.Core.BindableProperty<System.String>.onChange((val) =>
                {
                    ((Action<System.String>)act)(val);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.OnRequestFinishedDelegate>((act) =>
            {
                return new BestHTTP.OnRequestFinishedDelegate((originalRequest, response) =>
                {
                    ((Action<BestHTTP.HTTPRequest, BestHTTP.HTTPResponse>)act)(originalRequest, response);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.TweenCallback>((act) =>
            {
                return new DG.Tweening.TweenCallback(() =>
                {
                    ((Action)act)();
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
            {
                return new System.Comparison<ILRuntime.Runtime.Intepreter.ILTypeInstance>((x, y) =>
                {
                    return ((Func<ILRuntime.Runtime.Intepreter.ILTypeInstance, ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Int32>)act)(x, y);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOGetter<System.Single>>((act) =>
            {
                return new DG.Tweening.Core.DOGetter<System.Single>(() =>
                {
                    return ((Func<System.Single>)act)();
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOSetter<System.Single>>((act) =>
            {
                return new DG.Tweening.Core.DOSetter<System.Single>((pNewValue) =>
                {
                    ((Action<System.Single>)act)(pNewValue);
                });
            });

            appdomain.DelegateManager.RegisterFunctionDelegate<System.Int32>();

            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOGetter<System.Int32>>((act) =>
            {
                return new DG.Tweening.Core.DOGetter<System.Int32>(() =>
                {
                    return ((Func<System.Int32>)act)();
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOSetter<System.Int32>>((act) =>
            {
                return new DG.Tweening.Core.DOSetter<System.Int32>((pNewValue) =>
                {
                    ((Action<System.Int32>)act)(pNewValue);
                });
            });

            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Video.VideoPlayer, System.String>();

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Video.VideoPlayer.ErrorEventHandler>((act) =>
            {
                return new UnityEngine.Video.VideoPlayer.ErrorEventHandler((source, message) =>
                {
                    ((Action<UnityEngine.Video.VideoPlayer, System.String>)act)(source, message);
                });
            });

            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Video.VideoPlayer>();

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Video.VideoPlayer.EventHandler>((act) =>
            {
                return new UnityEngine.Video.VideoPlayer.EventHandler((source) =>
                {
                    ((Action<UnityEngine.Video.VideoPlayer>)act)(source);
                });
            });

            appdomain.DelegateManager.RegisterMethodDelegate<System.String, UnityEngine.GameObject>();
            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Vector2>();

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<UnityEngine.Vector2>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<UnityEngine.Vector2>((arg0) =>
                {
                    ((Action<UnityEngine.Vector2>)act)(arg0);
                });
            });
        }
    }
}