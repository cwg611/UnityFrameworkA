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
    unsafe class UnityEngine_UIVertex_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(UnityEngine.UIVertex);

            field = type.GetField("position", flag);
            app.RegisterCLRFieldGetter(field, get_position_0);
            app.RegisterCLRFieldSetter(field, set_position_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_position_0, AssignFromStack_position_0);
            field = type.GetField("uv1", flag);
            app.RegisterCLRFieldGetter(field, get_uv1_1);
            app.RegisterCLRFieldSetter(field, set_uv1_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_uv1_1, AssignFromStack_uv1_1);

            app.RegisterCLRMemberwiseClone(type, PerformMemberwiseClone);

            app.RegisterCLRCreateDefaultInstance(type, () => new UnityEngine.UIVertex());
            app.RegisterCLRCreateArrayInstance(type, s => new UnityEngine.UIVertex[s]);


        }

        static void WriteBackInstance(ILRuntime.Runtime.Enviorment.AppDomain __domain, StackObject* ptr_of_this_method, IList<object> __mStack, ref UnityEngine.UIVertex instance_of_this_method)
        {
            ptr_of_this_method = ILIntepreter.GetObjectAndResolveReference(ptr_of_this_method);
            switch(ptr_of_this_method->ObjectType)
            {
                case ObjectTypes.Object:
                    {
                        __mStack[ptr_of_this_method->Value] = instance_of_this_method;
                    }
                    break;
                case ObjectTypes.FieldReference:
                    {
                        var ___obj = __mStack[ptr_of_this_method->Value];
                        if(___obj is ILTypeInstance)
                        {
                            ((ILTypeInstance)___obj)[ptr_of_this_method->ValueLow] = instance_of_this_method;
                        }
                        else
                        {
                            var t = __domain.GetType(___obj.GetType()) as CLRType;
                            t.SetFieldValue(ptr_of_this_method->ValueLow, ref ___obj, instance_of_this_method);
                        }
                    }
                    break;
                case ObjectTypes.StaticFieldReference:
                    {
                        var t = __domain.GetType(ptr_of_this_method->Value);
                        if(t is ILType)
                        {
                            ((ILType)t).StaticInstance[ptr_of_this_method->ValueLow] = instance_of_this_method;
                        }
                        else
                        {
                            ((CLRType)t).SetStaticFieldValue(ptr_of_this_method->ValueLow, instance_of_this_method);
                        }
                    }
                    break;
                 case ObjectTypes.ArrayReference:
                    {
                        var instance_of_arrayReference = __mStack[ptr_of_this_method->Value] as UnityEngine.UIVertex[];
                        instance_of_arrayReference[ptr_of_this_method->ValueLow] = instance_of_this_method;
                    }
                    break;
            }
        }


        static object get_position_0(ref object o)
        {
            return ((UnityEngine.UIVertex)o).position;
        }

        static StackObject* CopyToStack_position_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((UnityEngine.UIVertex)o).position;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_position_0(ref object o, object v)
        {
            UnityEngine.UIVertex ins =(UnityEngine.UIVertex)o;
            ins.position = (UnityEngine.Vector3)v;
            o = ins;
        }

        static StackObject* AssignFromStack_position_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Vector3 @position = (UnityEngine.Vector3)typeof(UnityEngine.Vector3).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            UnityEngine.UIVertex ins =(UnityEngine.UIVertex)o;
            ins.position = @position;
            o = ins;
            return ptr_of_this_method;
        }

        static object get_uv1_1(ref object o)
        {
            return ((UnityEngine.UIVertex)o).uv1;
        }

        static StackObject* CopyToStack_uv1_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((UnityEngine.UIVertex)o).uv1;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_uv1_1(ref object o, object v)
        {
            UnityEngine.UIVertex ins =(UnityEngine.UIVertex)o;
            ins.uv1 = (UnityEngine.Vector2)v;
            o = ins;
        }

        static StackObject* AssignFromStack_uv1_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Vector2 @uv1 = (UnityEngine.Vector2)typeof(UnityEngine.Vector2).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            UnityEngine.UIVertex ins =(UnityEngine.UIVertex)o;
            ins.uv1 = @uv1;
            o = ins;
            return ptr_of_this_method;
        }


        static object PerformMemberwiseClone(ref object o)
        {
            var ins = new UnityEngine.UIVertex();
            ins = (UnityEngine.UIVertex)o;
            return ins;
        }


    }
}
