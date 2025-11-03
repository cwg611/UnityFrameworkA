using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class UnityEngine_Video_VideoPlayer_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(UnityEngine.Video.VideoPlayer);
            args = new Type[]{};
            method = type.GetMethod("get_time", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_time_0);
            args = new Type[]{};
            method = type.GetMethod("get_isPlaying", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_isPlaying_1);
            args = new Type[]{};
            method = type.GetMethod("get_isPrepared", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_isPrepared_2);
            args = new Type[]{};
            method = type.GetMethod("get_length", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_length_3);
            args = new Type[]{};
            method = type.GetMethod("get_frameCount", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_frameCount_4);
            args = new Type[]{};
            method = type.GetMethod("get_frameRate", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_frameRate_5);
            args = new Type[]{};
            method = type.GetMethod("get_url", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_url_6);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("set_url", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_url_7);
            args = new Type[]{};
            method = type.GetMethod("get_targetTexture", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_targetTexture_8);
            args = new Type[]{typeof(UnityEngine.RenderTexture)};
            method = type.GetMethod("set_targetTexture", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_targetTexture_9);
            args = new Type[]{typeof(System.UInt16)};
            method = type.GetMethod("GetDirectAudioVolume", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetDirectAudioVolume_10);
            args = new Type[]{typeof(System.UInt16), typeof(System.Single)};
            method = type.GetMethod("SetDirectAudioVolume", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetDirectAudioVolume_11);
            args = new Type[]{typeof(UnityEngine.Video.VideoSource)};
            method = type.GetMethod("set_source", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_source_12);
            args = new Type[]{};
            method = type.GetMethod("Prepare", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Prepare_13);
            args = new Type[]{typeof(UnityEngine.Video.VideoClip)};
            method = type.GetMethod("set_clip", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_clip_14);
            args = new Type[]{};
            method = type.GetMethod("Play", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Play_15);
            args = new Type[]{};
            method = type.GetMethod("Pause", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Pause_16);
            args = new Type[]{typeof(System.Double)};
            method = type.GetMethod("set_time", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_time_17);
            args = new Type[]{};
            method = type.GetMethod("get_audioTrackCount", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_audioTrackCount_18);
            args = new Type[]{};
            method = type.GetMethod("get_canSetDirectAudioVolume", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_canSetDirectAudioVolume_19);
            args = new Type[]{typeof(UnityEngine.Video.VideoAudioOutputMode)};
            method = type.GetMethod("set_audioOutputMode", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_audioOutputMode_20);
            args = new Type[]{typeof(System.UInt16), typeof(UnityEngine.AudioSource)};
            method = type.GetMethod("SetTargetAudioSource", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetTargetAudioSource_21);
            args = new Type[]{typeof(System.UInt16)};
            method = type.GetMethod("set_controlledAudioTrackCount", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_controlledAudioTrackCount_22);
            args = new Type[]{typeof(System.UInt16), typeof(System.Boolean)};
            method = type.GetMethod("EnableAudioTrack", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, EnableAudioTrack_23);
            args = new Type[]{typeof(UnityEngine.Video.VideoPlayer.ErrorEventHandler)};
            method = type.GetMethod("add_errorReceived", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, add_errorReceived_24);
            args = new Type[]{typeof(UnityEngine.Video.VideoPlayer.EventHandler)};
            method = type.GetMethod("add_prepareCompleted", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, add_prepareCompleted_25);
            args = new Type[]{typeof(UnityEngine.Video.VideoPlayer.EventHandler)};
            method = type.GetMethod("add_started", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, add_started_26);
            args = new Type[]{typeof(UnityEngine.Video.VideoPlayer.EventHandler)};
            method = type.GetMethod("add_loopPointReached", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, add_loopPointReached_27);
            args = new Type[]{typeof(UnityEngine.Video.VideoPlayer.ErrorEventHandler)};
            method = type.GetMethod("remove_errorReceived", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, remove_errorReceived_28);
            args = new Type[]{typeof(UnityEngine.Video.VideoPlayer.EventHandler)};
            method = type.GetMethod("remove_prepareCompleted", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, remove_prepareCompleted_29);
            args = new Type[]{typeof(UnityEngine.Video.VideoPlayer.EventHandler)};
            method = type.GetMethod("remove_started", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, remove_started_30);
            args = new Type[]{typeof(UnityEngine.Video.VideoPlayer.EventHandler)};
            method = type.GetMethod("remove_loopPointReached", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, remove_loopPointReached_31);


        }


        static StackObject* get_time_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.time;

            __ret->ObjectType = ObjectTypes.Double;
            *(double*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_isPlaying_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.isPlaying;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_isPrepared_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.isPrepared;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_length_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.length;

            __ret->ObjectType = ObjectTypes.Double;
            *(double*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_frameCount_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.frameCount;

            __ret->ObjectType = ObjectTypes.Long;
            *(ulong*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_frameRate_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.frameRate;

            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_url_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.url;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_url_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.url = value;

            return __ret;
        }

        static StackObject* get_targetTexture_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.targetTexture;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_targetTexture_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.RenderTexture @value = (UnityEngine.RenderTexture)typeof(UnityEngine.RenderTexture).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.targetTexture = value;

            return __ret;
        }

        static StackObject* GetDirectAudioVolume_10(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.UInt16 @trackIndex = (ushort)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetDirectAudioVolume(@trackIndex);

            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* SetDirectAudioVolume_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @volume = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.UInt16 @trackIndex = (ushort)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetDirectAudioVolume(@trackIndex, @volume);

            return __ret;
        }

        static StackObject* set_source_12(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoSource @value = (UnityEngine.Video.VideoSource)typeof(UnityEngine.Video.VideoSource).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.source = value;

            return __ret;
        }

        static StackObject* Prepare_13(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Prepare();

            return __ret;
        }

        static StackObject* set_clip_14(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoClip @value = (UnityEngine.Video.VideoClip)typeof(UnityEngine.Video.VideoClip).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.clip = value;

            return __ret;
        }

        static StackObject* Play_15(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Play();

            return __ret;
        }

        static StackObject* Pause_16(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Pause();

            return __ret;
        }

        static StackObject* set_time_17(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Double @value = *(double*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.time = value;

            return __ret;
        }

        static StackObject* get_audioTrackCount_18(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.audioTrackCount;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_canSetDirectAudioVolume_19(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.canSetDirectAudioVolume;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* set_audioOutputMode_20(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoAudioOutputMode @value = (UnityEngine.Video.VideoAudioOutputMode)typeof(UnityEngine.Video.VideoAudioOutputMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.audioOutputMode = value;

            return __ret;
        }

        static StackObject* SetTargetAudioSource_21(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.AudioSource @source = (UnityEngine.AudioSource)typeof(UnityEngine.AudioSource).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.UInt16 @trackIndex = (ushort)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetTargetAudioSource(@trackIndex, @source);

            return __ret;
        }

        static StackObject* set_controlledAudioTrackCount_22(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.UInt16 @value = (ushort)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.controlledAudioTrackCount = value;

            return __ret;
        }

        static StackObject* EnableAudioTrack_23(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @enabled = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.UInt16 @trackIndex = (ushort)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.EnableAudioTrack(@trackIndex, @enabled);

            return __ret;
        }

        static StackObject* add_errorReceived_24(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer.ErrorEventHandler @value = (UnityEngine.Video.VideoPlayer.ErrorEventHandler)typeof(UnityEngine.Video.VideoPlayer.ErrorEventHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.errorReceived += value;

            return __ret;
        }

        static StackObject* add_prepareCompleted_25(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer.EventHandler @value = (UnityEngine.Video.VideoPlayer.EventHandler)typeof(UnityEngine.Video.VideoPlayer.EventHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.prepareCompleted += value;

            return __ret;
        }

        static StackObject* add_started_26(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer.EventHandler @value = (UnityEngine.Video.VideoPlayer.EventHandler)typeof(UnityEngine.Video.VideoPlayer.EventHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.started += value;

            return __ret;
        }

        static StackObject* add_loopPointReached_27(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer.EventHandler @value = (UnityEngine.Video.VideoPlayer.EventHandler)typeof(UnityEngine.Video.VideoPlayer.EventHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.loopPointReached += value;

            return __ret;
        }

        static StackObject* remove_errorReceived_28(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer.ErrorEventHandler @value = (UnityEngine.Video.VideoPlayer.ErrorEventHandler)typeof(UnityEngine.Video.VideoPlayer.ErrorEventHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.errorReceived -= value;

            return __ret;
        }

        static StackObject* remove_prepareCompleted_29(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer.EventHandler @value = (UnityEngine.Video.VideoPlayer.EventHandler)typeof(UnityEngine.Video.VideoPlayer.EventHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.prepareCompleted -= value;

            return __ret;
        }

        static StackObject* remove_started_30(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer.EventHandler @value = (UnityEngine.Video.VideoPlayer.EventHandler)typeof(UnityEngine.Video.VideoPlayer.EventHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.started -= value;

            return __ret;
        }

        static StackObject* remove_loopPointReached_31(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Video.VideoPlayer.EventHandler @value = (UnityEngine.Video.VideoPlayer.EventHandler)typeof(UnityEngine.Video.VideoPlayer.EventHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Video.VideoPlayer instance_of_this_method = (UnityEngine.Video.VideoPlayer)typeof(UnityEngine.Video.VideoPlayer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.loopPointReached -= value;

            return __ret;
        }



    }
}
