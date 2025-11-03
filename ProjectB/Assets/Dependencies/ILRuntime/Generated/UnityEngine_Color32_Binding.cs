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
    unsafe class UnityEngine_Color32_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(UnityEngine.Color32);
            MethodInfo[] methods = type.GetMethods(flag).Where(t => !t.IsGenericMethod).ToArray();
            args = new Type[]{typeof(UnityEngine.Color32)};
            method = methods.Where(t => t.Name.Equals("op_Implicit") && t.ReturnType == typeof(UnityEngine.Color) && t.CheckMethodParams(args)).Single();
            app.RegisterCLRMethodRedirection(method, op_Implicit_0);

            field = type.GetField("r", flag);
            app.RegisterCLRFieldGetter(field, get_r_0);
            app.RegisterCLRFieldSetter(field, set_r_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_r_0, AssignFromStack_r_0);
            field = type.GetField("g", flag);
            app.RegisterCLRFieldGetter(field, get_g_1);
            app.RegisterCLRFieldSetter(field, set_g_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_g_1, AssignFromStack_g_1);
            field = type.GetField("b", flag);
            app.RegisterCLRFieldGetter(field, get_b_2);
            app.RegisterCLRFieldSetter(field, set_b_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_b_2, AssignFromStack_b_2);

            app.RegisterCLRCreateDefaultInstance(type, () => new UnityEngine.Color32());
            app.RegisterCLRCreateArrayInstance(type, s => new UnityEngine.Color32[s]);

            args = new Type[]{typeof(System.Byte), typeof(System.Byte), typeof(System.Byte), typeof(System.Byte)};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }

        static void WriteBackInstance(ILRuntime.Runtime.Enviorment.AppDomain __domain, StackObject* ptr_of_this_method, IList<object> __mStack, ref UnityEngine.Color32 instance_of_this_method)
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
                        var instance_of_arrayReference = __mStack[ptr_of_this_method->Value] as UnityEngine.Color32[];
                        instance_of_arrayReference[ptr_of_this_method->ValueLow] = instance_of_this_method;
                    }
                    break;
            }
        }

        static StackObject* op_Implicit_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Color32 @c = (UnityEngine.Color32)typeof(UnityEngine.Color32).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = (UnityEngine.Color)c;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object get_r_0(ref object o)
        {
            return ((UnityEngine.Color32)o).r;
        }

        static StackObject* CopyToStack_r_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((UnityEngine.Color32)o).r;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_r_0(ref object o, object v)
        {
            UnityEngine.Color32 ins =(UnityEngine.Color32)o;
            ins.r = (System.Byte)v;
            o = ins;
        }

        static StackObject* AssignFromStack_r_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Byte @r = (byte)ptr_of_this_method->Value;
            UnityEngine.Color32 ins =(UnityEngine.Color32)o;
            ins.r = @r;
            o = ins;
            return ptr_of_this_method;
        }

        static object get_g_1(ref object o)
        {
            return ((UnityEngine.Color32)o).g;
        }

        static StackObject* CopyToStack_g_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((UnityEngine.Color32)o).g;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_g_1(ref object o, object v)
        {
            UnityEngine.Color32 ins =(UnityEngine.Color32)o;
            ins.g = (System.Byte)v;
            o = ins;
        }

        static StackObject* AssignFromStack_g_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Byte @g = (byte)ptr_of_this_method->Value;
            UnityEngine.Color32 ins =(UnityEngine.Color32)o;
            ins.g = @g;
            o = ins;
            return ptr_of_this_method;
        }

        static object get_b_2(ref object o)
        {
            return ((UnityEngine.Color32)o).b;
        }

        static StackObject* CopyToStack_b_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((UnityEngine.Color32)o).b;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_b_2(ref object o, object v)
        {
            UnityEngine.Color32 ins =(UnityEngine.Color32)o;
            ins.b = (System.Byte)v;
            o = ins;
        }

        static StackObject* AssignFromStack_b_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Byte @b = (byte)ptr_of_this_method->Value;
            UnityEngine.Color32 ins =(UnityEngine.Color32)o;
            ins.b = @b;
            o = ins;
            return ptr_of_this_method;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Byte @a = (byte)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Byte @b = (byte)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Byte @g = (byte)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            System.Byte @r = (byte)ptr_of_this_method->Value;


            var result_of_this_method = new UnityEngine.Color32(@r, @g, @b, @a);

            if(!isNewObj)
            {
                __ret--;
                WriteBackInstance(__domain, __ret, __mStack, ref result_of_this_method);
                return __ret;
            }

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
