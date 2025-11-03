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
    unsafe class MsgBaseAPI_3_MsgINAVLocalAndHotupt_Object_MsgINAVLocalAndHotuptCode_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode>);
            args = new Type[]{typeof(global::MsgINAVLocalAndHotuptCode), typeof(System.Action<System.Object>)};
            method = type.GetMethod("AddEventListener", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, AddEventListener_0);
            args = new Type[]{typeof(global::MsgINAVLocalAndHotuptCode), typeof(System.Object)};
            method = type.GetMethod("Dispatch", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Dispatch_1);
            args = new Type[]{typeof(global::MsgINAVLocalAndHotuptCode), typeof(System.Action<System.Object>)};
            method = type.GetMethod("RemoveEventListener", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, RemoveEventListener_2);


        }


        static StackObject* AddEventListener_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Object> @handle = (System.Action<System.Object>)typeof(System.Action<System.Object>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            global::MsgINAVLocalAndHotuptCode @key = (global::MsgINAVLocalAndHotuptCode)typeof(global::MsgINAVLocalAndHotuptCode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode> instance_of_this_method = (global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode>)typeof(global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.AddEventListener(@key, @handle);

            return __ret;
        }

        static StackObject* Dispatch_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object @p = (System.Object)typeof(System.Object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            global::MsgINAVLocalAndHotuptCode @key = (global::MsgINAVLocalAndHotuptCode)typeof(global::MsgINAVLocalAndHotuptCode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode> instance_of_this_method = (global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode>)typeof(global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Dispatch(@key, @p);

            return __ret;
        }

        static StackObject* RemoveEventListener_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Object> @handle = (System.Action<System.Object>)typeof(System.Action<System.Object>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            global::MsgINAVLocalAndHotuptCode @key = (global::MsgINAVLocalAndHotuptCode)typeof(global::MsgINAVLocalAndHotuptCode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode> instance_of_this_method = (global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode>)typeof(global::MsgBaseAPI<global::MsgINAVLocalAndHotupt, System.Object, global::MsgINAVLocalAndHotuptCode>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.RemoveEventListener(@key, @handle);

            return __ret;
        }



    }
}
