using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;
using System.Threading.Tasks;

namespace ProjectAdapter
{   
    public class TextAdapter : CrossBindingAdaptor
    {
        static CrossBindingFunctionInfo<UnityEngine.Texture> mget_mainTexture_0 = new CrossBindingFunctionInfo<UnityEngine.Texture>("get_mainTexture");
        static CrossBindingFunctionInfo<System.String> mget_text_1 = new CrossBindingFunctionInfo<System.String>("get_text");
        static CrossBindingMethodInfo<System.String> mset_text_2 = new CrossBindingMethodInfo<System.String>("set_text");
        static CrossBindingMethodInfo mOnEnable_3 = new CrossBindingMethodInfo("OnEnable");
        static CrossBindingMethodInfo mOnDisable_4 = new CrossBindingMethodInfo("OnDisable");
        static CrossBindingMethodInfo mUpdateGeometry_5 = new CrossBindingMethodInfo("UpdateGeometry");
        static CrossBindingMethodInfo mReset_6 = new CrossBindingMethodInfo("Reset");
        static CrossBindingMethodInfo<UnityEngine.UI.VertexHelper> mOnPopulateMesh_7 = new CrossBindingMethodInfo<UnityEngine.UI.VertexHelper>("OnPopulateMesh");
        static CrossBindingMethodInfo mCalculateLayoutInputHorizontal_8 = new CrossBindingMethodInfo("CalculateLayoutInputHorizontal");
        static CrossBindingMethodInfo mCalculateLayoutInputVertical_9 = new CrossBindingMethodInfo("CalculateLayoutInputVertical");
        static CrossBindingFunctionInfo<System.Single> mget_minWidth_10 = new CrossBindingFunctionInfo<System.Single>("get_minWidth");
        static CrossBindingFunctionInfo<System.Single> mget_preferredWidth_11 = new CrossBindingFunctionInfo<System.Single>("get_preferredWidth");
        static CrossBindingFunctionInfo<System.Single> mget_flexibleWidth_12 = new CrossBindingFunctionInfo<System.Single>("get_flexibleWidth");
        static CrossBindingFunctionInfo<System.Single> mget_minHeight_13 = new CrossBindingFunctionInfo<System.Single>("get_minHeight");
        static CrossBindingFunctionInfo<System.Single> mget_preferredHeight_14 = new CrossBindingFunctionInfo<System.Single>("get_preferredHeight");
        static CrossBindingFunctionInfo<System.Single> mget_flexibleHeight_15 = new CrossBindingFunctionInfo<System.Single>("get_flexibleHeight");
        static CrossBindingFunctionInfo<System.Int32> mget_layoutPriority_16 = new CrossBindingFunctionInfo<System.Int32>("get_layoutPriority");
        static CrossBindingMethodInfo mOnRebuildRequested_17 = new CrossBindingMethodInfo("OnRebuildRequested");
        static CrossBindingMethodInfo mOnValidate_18 = new CrossBindingMethodInfo("OnValidate");
        static CrossBindingFunctionInfo<UnityEngine.Material, UnityEngine.Material> mGetModifiedMaterial_19 = new CrossBindingFunctionInfo<UnityEngine.Material, UnityEngine.Material>("GetModifiedMaterial");
        static CrossBindingMethodInfo<UnityEngine.Rect, System.Boolean> mCull_20 = new CrossBindingMethodInfo<UnityEngine.Rect, System.Boolean>("Cull");
        static CrossBindingMethodInfo<UnityEngine.Rect, System.Boolean> mSetClipRect_21 = new CrossBindingMethodInfo<UnityEngine.Rect, System.Boolean>("SetClipRect");
        static CrossBindingMethodInfo mOnTransformParentChanged_22 = new CrossBindingMethodInfo("OnTransformParentChanged");
        static CrossBindingMethodInfo mOnCanvasHierarchyChanged_23 = new CrossBindingMethodInfo("OnCanvasHierarchyChanged");
        static CrossBindingMethodInfo mRecalculateClipping_24 = new CrossBindingMethodInfo("RecalculateClipping");
        static CrossBindingMethodInfo mRecalculateMasking_25 = new CrossBindingMethodInfo("RecalculateMasking");
        static CrossBindingFunctionInfo<UnityEngine.Color> mget_color_26 = new CrossBindingFunctionInfo<UnityEngine.Color>("get_color");
        static CrossBindingMethodInfo<UnityEngine.Color> mset_color_27 = new CrossBindingMethodInfo<UnityEngine.Color>("set_color");
        static CrossBindingFunctionInfo<System.Boolean> mget_raycastTarget_28 = new CrossBindingFunctionInfo<System.Boolean>("get_raycastTarget");
        static CrossBindingMethodInfo<System.Boolean> mset_raycastTarget_29 = new CrossBindingMethodInfo<System.Boolean>("set_raycastTarget");
        static CrossBindingMethodInfo mSetAllDirty_30 = new CrossBindingMethodInfo("SetAllDirty");
        static CrossBindingMethodInfo mSetLayoutDirty_31 = new CrossBindingMethodInfo("SetLayoutDirty");
        static CrossBindingMethodInfo mSetVerticesDirty_32 = new CrossBindingMethodInfo("SetVerticesDirty");
        static CrossBindingMethodInfo mSetMaterialDirty_33 = new CrossBindingMethodInfo("SetMaterialDirty");
        static CrossBindingMethodInfo mOnRectTransformDimensionsChange_34 = new CrossBindingMethodInfo("OnRectTransformDimensionsChange");
        static CrossBindingMethodInfo mOnBeforeTransformParentChanged_35 = new CrossBindingMethodInfo("OnBeforeTransformParentChanged");
        static CrossBindingFunctionInfo<UnityEngine.Material> mget_defaultMaterial_36 = new CrossBindingFunctionInfo<UnityEngine.Material>("get_defaultMaterial");
        static CrossBindingFunctionInfo<UnityEngine.Material> mget_material_37 = new CrossBindingFunctionInfo<UnityEngine.Material>("get_material");
        static CrossBindingMethodInfo<UnityEngine.Material> mset_material_38 = new CrossBindingMethodInfo<UnityEngine.Material>("set_material");
        static CrossBindingFunctionInfo<UnityEngine.Material> mget_materialForRendering_39 = new CrossBindingFunctionInfo<UnityEngine.Material>("get_materialForRendering");
        static CrossBindingMethodInfo mOnDestroy_40 = new CrossBindingMethodInfo("OnDestroy");
        static CrossBindingMethodInfo mOnCullingChanged_41 = new CrossBindingMethodInfo("OnCullingChanged");
        static CrossBindingMethodInfo<UnityEngine.UI.CanvasUpdate> mRebuild_42 = new CrossBindingMethodInfo<UnityEngine.UI.CanvasUpdate>("Rebuild");
        static CrossBindingMethodInfo mLayoutComplete_43 = new CrossBindingMethodInfo("LayoutComplete");
        static CrossBindingMethodInfo mGraphicUpdateComplete_44 = new CrossBindingMethodInfo("GraphicUpdateComplete");
        static CrossBindingMethodInfo mUpdateMaterial_45 = new CrossBindingMethodInfo("UpdateMaterial");
        static CrossBindingMethodInfo mOnDidApplyAnimationProperties_46 = new CrossBindingMethodInfo("OnDidApplyAnimationProperties");
        static CrossBindingMethodInfo mSetNativeSize_47 = new CrossBindingMethodInfo("SetNativeSize");
        static CrossBindingFunctionInfo<UnityEngine.Vector2, UnityEngine.Camera, System.Boolean> mRaycast_48 = new CrossBindingFunctionInfo<UnityEngine.Vector2, UnityEngine.Camera, System.Boolean>("Raycast");
        static CrossBindingMethodInfo<UnityEngine.Color, System.Single, System.Boolean, System.Boolean> mCrossFadeColor_49 = new CrossBindingMethodInfo<UnityEngine.Color, System.Single, System.Boolean, System.Boolean>("CrossFadeColor");
        static CrossBindingMethodInfo<UnityEngine.Color, System.Single, System.Boolean, System.Boolean, System.Boolean> mCrossFadeColor_50 = new CrossBindingMethodInfo<UnityEngine.Color, System.Single, System.Boolean, System.Boolean, System.Boolean>("CrossFadeColor");
        static CrossBindingMethodInfo<System.Single, System.Single, System.Boolean> mCrossFadeAlpha_51 = new CrossBindingMethodInfo<System.Single, System.Single, System.Boolean>("CrossFadeAlpha");
        static CrossBindingMethodInfo mAwake_52 = new CrossBindingMethodInfo("Awake");
        static CrossBindingMethodInfo mStart_53 = new CrossBindingMethodInfo("Start");
        static CrossBindingFunctionInfo<System.Boolean> mIsActive_54 = new CrossBindingFunctionInfo<System.Boolean>("IsActive");
        static CrossBindingMethodInfo mOnCanvasGroupChanged_55 = new CrossBindingMethodInfo("OnCanvasGroupChanged");
        static CrossBindingFunctionInfo<UnityEngine.Transform> mget_transform_56 = new CrossBindingFunctionInfo<UnityEngine.Transform>("get_transform");
        static CrossBindingFunctionInfo<UnityEngine.GameObject> mget_gameObject_57 = new CrossBindingFunctionInfo<UnityEngine.GameObject>("get_gameObject");
        public override Type BaseCLRType
        {
            get
            {
                return typeof(UnityEngine.UI.Text);
            }
        }

        public override Type AdaptorType
        {
            get
            {
                return typeof(Adapter);
            }
        }

        public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            return new Adapter(appdomain, instance);
        }

        public class Adapter : UnityEngine.UI.Text, CrossBindingAdaptorType
        {
            ILTypeInstance instance;
            ILRuntime.Runtime.Enviorment.AppDomain appdomain;

            public Adapter()
            {

            }

            public Adapter(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            public ILTypeInstance ILInstance { get { return instance; } }

/*            protected override void OnEnable()
            {
                if (mOnEnable_3.CheckShouldInvokeBase(this.instance))
                    base.OnEnable();
                else
                    mOnEnable_3.Invoke(this.instance);
            }

            protected override void OnDisable()
            {
                if (mOnDisable_4.CheckShouldInvokeBase(this.instance))
                    base.OnDisable();
                else
                    mOnDisable_4.Invoke(this.instance);
            }*/

            protected override void UpdateGeometry()
            {
                if (mUpdateGeometry_5.CheckShouldInvokeBase(this.instance))
                    base.UpdateGeometry();
                else
                    mUpdateGeometry_5.Invoke(this.instance);
            }

/*            protected override void Reset()
            {
                if (mReset_6.CheckShouldInvokeBase(this.instance))
                    base.Reset();
                else
                    mReset_6.Invoke(this.instance);
            }*/

            protected override void OnPopulateMesh(UnityEngine.UI.VertexHelper toFill)
            {
                if (mOnPopulateMesh_7.CheckShouldInvokeBase(this.instance))
                    base.OnPopulateMesh(toFill);
                else
                    mOnPopulateMesh_7.Invoke(this.instance, toFill);
            }

            public override void CalculateLayoutInputHorizontal()
            {
                if (mCalculateLayoutInputHorizontal_8.CheckShouldInvokeBase(this.instance))
                    base.CalculateLayoutInputHorizontal();
                else
                    mCalculateLayoutInputHorizontal_8.Invoke(this.instance);
            }

            public override void CalculateLayoutInputVertical()
            {
                if (mCalculateLayoutInputVertical_9.CheckShouldInvokeBase(this.instance))
                    base.CalculateLayoutInputVertical();
                else
                    mCalculateLayoutInputVertical_9.Invoke(this.instance);
            }

/*            public override void OnRebuildRequested()
            {
                if (mOnRebuildRequested_17.CheckShouldInvokeBase(this.instance))
                    base.OnRebuildRequested();
                else
                    mOnRebuildRequested_17.Invoke(this.instance);
            }*/

            /*            protected override void OnValidate()
                        {
                            if (mOnValidate_18.CheckShouldInvokeBase(this.instance))
                                base.OnValidate();
                            else
                                mOnValidate_18.Invoke(this.instance);
                        }*/

            public override UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial)
            {
                if (mGetModifiedMaterial_19.CheckShouldInvokeBase(this.instance))
                    return base.GetModifiedMaterial(baseMaterial);
                else
                    return mGetModifiedMaterial_19.Invoke(this.instance, baseMaterial);
            }

            public override void Cull(UnityEngine.Rect clipRect, System.Boolean validRect)
            {
                if (mCull_20.CheckShouldInvokeBase(this.instance))
                    base.Cull(clipRect, validRect);
                else
                    mCull_20.Invoke(this.instance, clipRect, validRect);
            }

            public override void SetClipRect(UnityEngine.Rect clipRect, System.Boolean validRect)
            {
                if (mSetClipRect_21.CheckShouldInvokeBase(this.instance))
                    base.SetClipRect(clipRect, validRect);
                else
                    mSetClipRect_21.Invoke(this.instance, clipRect, validRect);
            }

/*            protected override void OnTransformParentChanged()
            {
                if (mOnTransformParentChanged_22.CheckShouldInvokeBase(this.instance))
                    base.OnTransformParentChanged();
                else
                    mOnTransformParentChanged_22.Invoke(this.instance);
            }*/

/*            protected override void OnCanvasHierarchyChanged()
            {
                if (mOnCanvasHierarchyChanged_23.CheckShouldInvokeBase(this.instance))
                    base.OnCanvasHierarchyChanged();
                else
                    mOnCanvasHierarchyChanged_23.Invoke(this.instance);
            }*/

            public override void RecalculateClipping()
            {
                if (mRecalculateClipping_24.CheckShouldInvokeBase(this.instance))
                    base.RecalculateClipping();
                else
                    mRecalculateClipping_24.Invoke(this.instance);
            }

            public override void RecalculateMasking()
            {
                if (mRecalculateMasking_25.CheckShouldInvokeBase(this.instance))
                    base.RecalculateMasking();
                else
                    mRecalculateMasking_25.Invoke(this.instance);
            }

            public override void SetAllDirty()
            {
                if (mSetAllDirty_30.CheckShouldInvokeBase(this.instance))
                    base.SetAllDirty();
                else
                    mSetAllDirty_30.Invoke(this.instance);
            }

            public override void SetLayoutDirty()
            {
                if (mSetLayoutDirty_31.CheckShouldInvokeBase(this.instance))
                    base.SetLayoutDirty();
                else
                    mSetLayoutDirty_31.Invoke(this.instance);
            }

            public override void SetVerticesDirty()
            {
                if (mSetVerticesDirty_32.CheckShouldInvokeBase(this.instance))
                    base.SetVerticesDirty();
                else
                    mSetVerticesDirty_32.Invoke(this.instance);
            }

            public override void SetMaterialDirty()
            {
                if (mSetMaterialDirty_33.CheckShouldInvokeBase(this.instance))
                    base.SetMaterialDirty();
                else
                    mSetMaterialDirty_33.Invoke(this.instance);
            }

/*            protected override void OnRectTransformDimensionsChange()
            {
                if (mOnRectTransformDimensionsChange_34.CheckShouldInvokeBase(this.instance))
                    base.OnRectTransformDimensionsChange();
                else
                    mOnRectTransformDimensionsChange_34.Invoke(this.instance);
            }*/

/*            protected override void OnBeforeTransformParentChanged()
            {
                if (mOnBeforeTransformParentChanged_35.CheckShouldInvokeBase(this.instance))
                    base.OnBeforeTransformParentChanged();
                else
                    mOnBeforeTransformParentChanged_35.Invoke(this.instance);
            }*/

/*            protected override void OnDestroy()
            {
                if (mOnDestroy_40.CheckShouldInvokeBase(this.instance))
                    base.OnDestroy();
                else
                    mOnDestroy_40.Invoke(this.instance);
            }*/

            public override void OnCullingChanged()
            {
                if (mOnCullingChanged_41.CheckShouldInvokeBase(this.instance))
                    base.OnCullingChanged();
                else
                    mOnCullingChanged_41.Invoke(this.instance);
            }

            public override void Rebuild(UnityEngine.UI.CanvasUpdate update)
            {
                if (mRebuild_42.CheckShouldInvokeBase(this.instance))
                    base.Rebuild(update);
                else
                    mRebuild_42.Invoke(this.instance, update);
            }

            public override void LayoutComplete()
            {
                if (mLayoutComplete_43.CheckShouldInvokeBase(this.instance))
                    base.LayoutComplete();
                else
                    mLayoutComplete_43.Invoke(this.instance);
            }

            public override void GraphicUpdateComplete()
            {
                if (mGraphicUpdateComplete_44.CheckShouldInvokeBase(this.instance))
                    base.GraphicUpdateComplete();
                else
                    mGraphicUpdateComplete_44.Invoke(this.instance);
            }

            protected override void UpdateMaterial()
            {
                if (mUpdateMaterial_45.CheckShouldInvokeBase(this.instance))
                    base.UpdateMaterial();
                else
                    mUpdateMaterial_45.Invoke(this.instance);
            }

/*            protected override void OnDidApplyAnimationProperties()
            {
                if (mOnDidApplyAnimationProperties_46.CheckShouldInvokeBase(this.instance))
                    base.OnDidApplyAnimationProperties();
                else
                    mOnDidApplyAnimationProperties_46.Invoke(this.instance);
            }*/

            public override void SetNativeSize()
            {
                if (mSetNativeSize_47.CheckShouldInvokeBase(this.instance))
                    base.SetNativeSize();
                else
                    mSetNativeSize_47.Invoke(this.instance);
            }

            public override System.Boolean Raycast(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera)
            {
                if (mRaycast_48.CheckShouldInvokeBase(this.instance))
                    return base.Raycast(sp, eventCamera);
                else
                    return mRaycast_48.Invoke(this.instance, sp, eventCamera);
            }

            public override void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha)
            {
                if (mCrossFadeColor_49.CheckShouldInvokeBase(this.instance))
                    base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
                else
                    mCrossFadeColor_49.Invoke(this.instance, targetColor, duration, ignoreTimeScale, useAlpha);
            }

            public override void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha, System.Boolean useRGB)
            {
                if (mCrossFadeColor_50.CheckShouldInvokeBase(this.instance))
                    base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, useRGB);
                else
                    mCrossFadeColor_50.Invoke(this.instance, targetColor, duration, ignoreTimeScale, useAlpha, useRGB);
            }

            public override void CrossFadeAlpha(System.Single alpha, System.Single duration, System.Boolean ignoreTimeScale)
            {
                if (mCrossFadeAlpha_51.CheckShouldInvokeBase(this.instance))
                    base.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
                else
                    mCrossFadeAlpha_51.Invoke(this.instance, alpha, duration, ignoreTimeScale);
            }

/*            protected override void Awake()
            {
                if (mAwake_52.CheckShouldInvokeBase(this.instance))
                    base.Awake();
                else
                    mAwake_52.Invoke(this.instance);
            }*/

/*            protected override void Start()
            {
                if (mStart_53.CheckShouldInvokeBase(this.instance))
                    base.Start();
                else
                    mStart_53.Invoke(this.instance);
            }*/

            public override System.Boolean IsActive()
            {
                if (mIsActive_54.CheckShouldInvokeBase(this.instance))
                    return base.IsActive();
                else
                    return mIsActive_54.Invoke(this.instance);
            }

/*            protected override void OnCanvasGroupChanged()
            {
                if (mOnCanvasGroupChanged_55.CheckShouldInvokeBase(this.instance))
                    base.OnCanvasGroupChanged();
                else
                    mOnCanvasGroupChanged_55.Invoke(this.instance);
            }*/

            public override UnityEngine.Texture mainTexture
            {
            get
            {
                if (mget_mainTexture_0.CheckShouldInvokeBase(this.instance))
                    return base.mainTexture;
                else
                    return mget_mainTexture_0.Invoke(this.instance);

            }
            }

            public override System.String text
            {
            get
            {
                if (mget_text_1.CheckShouldInvokeBase(this.instance))
                    return base.text;
                else
                    return mget_text_1.Invoke(this.instance);

            }
            set
            {
                if (mset_text_2.CheckShouldInvokeBase(this.instance))
                    base.text = value;
                else
                    mset_text_2.Invoke(this.instance, value);

            }
            }

            public override System.Single minWidth
            {
            get
            {
                if (mget_minWidth_10.CheckShouldInvokeBase(this.instance))
                    return base.minWidth;
                else
                    return mget_minWidth_10.Invoke(this.instance);

            }
            }

            public override System.Single preferredWidth
            {
            get
            {
                if (mget_preferredWidth_11.CheckShouldInvokeBase(this.instance))
                    return base.preferredWidth;
                else
                    return mget_preferredWidth_11.Invoke(this.instance);

            }
            }

            public override System.Single flexibleWidth
            {
            get
            {
                if (mget_flexibleWidth_12.CheckShouldInvokeBase(this.instance))
                    return base.flexibleWidth;
                else
                    return mget_flexibleWidth_12.Invoke(this.instance);

            }
            }

            public override System.Single minHeight
            {
            get
            {
                if (mget_minHeight_13.CheckShouldInvokeBase(this.instance))
                    return base.minHeight;
                else
                    return mget_minHeight_13.Invoke(this.instance);

            }
            }

            public override System.Single preferredHeight
            {
            get
            {
                if (mget_preferredHeight_14.CheckShouldInvokeBase(this.instance))
                    return base.preferredHeight;
                else
                    return mget_preferredHeight_14.Invoke(this.instance);

            }
            }

            public override System.Single flexibleHeight
            {
            get
            {
                if (mget_flexibleHeight_15.CheckShouldInvokeBase(this.instance))
                    return base.flexibleHeight;
                else
                    return mget_flexibleHeight_15.Invoke(this.instance);

            }
            }

            public override System.Int32 layoutPriority
            {
            get
            {
                if (mget_layoutPriority_16.CheckShouldInvokeBase(this.instance))
                    return base.layoutPriority;
                else
                    return mget_layoutPriority_16.Invoke(this.instance);

            }
            }

            public override UnityEngine.Color color
            {
            get
            {
                if (mget_color_26.CheckShouldInvokeBase(this.instance))
                    return base.color;
                else
                    return mget_color_26.Invoke(this.instance);

            }
            set
            {
                if (mset_color_27.CheckShouldInvokeBase(this.instance))
                    base.color = value;
                else
                    mset_color_27.Invoke(this.instance, value);

            }
            }

            public override System.Boolean raycastTarget
            {
            get
            {
                if (mget_raycastTarget_28.CheckShouldInvokeBase(this.instance))
                    return base.raycastTarget;
                else
                    return mget_raycastTarget_28.Invoke(this.instance);

            }
            set
            {
                if (mset_raycastTarget_29.CheckShouldInvokeBase(this.instance))
                    base.raycastTarget = value;
                else
                    mset_raycastTarget_29.Invoke(this.instance, value);

            }
            }

            public override UnityEngine.Material defaultMaterial
            {
            get
            {
                if (mget_defaultMaterial_36.CheckShouldInvokeBase(this.instance))
                    return base.defaultMaterial;
                else
                    return mget_defaultMaterial_36.Invoke(this.instance);

            }
            }

            public override UnityEngine.Material material
            {
            get
            {
                if (mget_material_37.CheckShouldInvokeBase(this.instance))
                    return base.material;
                else
                    return mget_material_37.Invoke(this.instance);

            }
            set
            {
                if (mset_material_38.CheckShouldInvokeBase(this.instance))
                    base.material = value;
                else
                    mset_material_38.Invoke(this.instance, value);

            }
            }

            public override UnityEngine.Material materialForRendering
            {
            get
            {
                if (mget_materialForRendering_39.CheckShouldInvokeBase(this.instance))
                    return base.materialForRendering;
                else
                    return mget_materialForRendering_39.Invoke(this.instance);

            }
            }

            public UnityEngine.Transform transform
            {
            get
            {
                return mget_transform_56.Invoke(this.instance);

            }
            }

            public UnityEngine.GameObject gameObject
            {
            get
            {
                return mget_gameObject_57.Invoke(this.instance);

            }
            }

            #region Generate For Mono Events from template
            /*
             * JEngine作者匠心打造的适配mono方法模板
             * 这里开始都是JEngine提供的模板自动生成的，注册了全部Mono的Event Methods，共几十个
             * 这么多框架，只有JEngine如此贴心，为你想到了一切可能
             * 你还有什么理由不去用JEngine？
             */
            object[] param0 = new object[0];
            private bool destoryed = false;
            
            IMethod mAwakeMethod;
            bool mAwakeMethodGot;private bool awaked = false;
            private bool isAwaking = false;
            
            public async void Awake()
            {
                try
                {
                    //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
                    if (instance != null)
                    {
                        if (!mAwakeMethodGot)
                        {
                            mAwakeMethod = instance.Type.GetMethod("Awake", 0);
                            mAwakeMethodGot = true;
                        }

                        if (mAwakeMethod != null && !isAwaking)
                        {
                            isAwaking = true;
                            //没激活就别awake
                            try
                            {
                                while (Application.isPlaying && !destoryed && !gameObject.activeInHierarchy)
                                {
                                    await Task.Delay(20);
                                }
                            }
                            catch (MissingReferenceException) //如果gameObject被删了，就会触发这个，这个时候就直接return了
                            {
                                return;
                            }

                            if (destoryed || !Application.isPlaying)
                            {
                                return;
                            }

                            appdomain.Invoke(mAwakeMethod, instance, param0);
                            isAwaking = false;
                            awaked = true;
                            OnEnable();
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    //如果出现了Null，那就重新Awake
                    Awake();
                }
            }
            
            IMethod mStartMethod;
            bool mStartMethodGot;
            void Start()
            {
                if (!mStartMethodGot)
                {
                    mStartMethod = instance.Type.GetMethod("Start", 0);
                    mStartMethodGot = true;
                }
            
                if (mStartMethod != null)
                {
                    appdomain.Invoke(mStartMethod, instance, param0);
                }
            }
            
            IMethod mUpdateMethod;
            bool mUpdateMethodGot;
            void Update()
            {
                if (!mUpdateMethodGot)
                {
                    mUpdateMethod = instance.Type.GetMethod("Update", 0);
                    mUpdateMethodGot = true;
                }
            
                if (mUpdateMethod != null)
                {
                    appdomain.Invoke(mUpdateMethod, instance, param0);
                }
            
            }
            
            IMethod mFixedUpdateMethod;
            bool mFixedUpdateMethodGot;
            void FixedUpdate()
            {
                if (!mFixedUpdateMethodGot)
                {
                    mFixedUpdateMethod = instance.Type.GetMethod("FixedUpdate", 0);
                    mFixedUpdateMethodGot = true;
                }
            
                if (mFixedUpdateMethod != null)
                {
                    appdomain.Invoke(mFixedUpdateMethod, instance, param0);
                }
            }
            
            IMethod mLateUpdateMethod;
            bool mLateUpdateMethodGot;
            void LateUpdate()
            {
                if (!mLateUpdateMethodGot)
                {
                    mLateUpdateMethod = instance.Type.GetMethod("LateUpdate", 0);
                    mLateUpdateMethodGot = true;
                }
            
                if (mLateUpdateMethod != null)
                {
                    appdomain.Invoke(mLateUpdateMethod, instance, param0);
                }
            }
            
            IMethod mOnEnableMethod;
            bool mOnEnableMethodGot;
            void OnEnable()
            {
                if (instance != null)
                {
                    if (!mOnEnableMethodGot)
                    {
                        mOnEnableMethod = instance.Type.GetMethod("OnEnable", 0);
                        mOnEnableMethodGot = true;
                    }
            
                    if (mOnEnableMethod != null && awaked)
                    {
                        appdomain.Invoke(mOnEnableMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnDisableMethod;
            bool mOnDisableMethodGot;
            void OnDisable()
            {
                if (instance != null)
                {
                    if (!mOnDisableMethodGot)
                    {
                        mOnDisableMethod = instance.Type.GetMethod("OnDisable", 0);
                        mOnDisableMethodGot = true;
                    }
            
                    if (mOnDisableMethod != null)
                    {
                        appdomain.Invoke(mOnDisableMethod, instance, param0);
                    }
                }
            }
            
            IMethod mDestroyMethod;
            bool mDestroyMethodGot;
            void OnDestroy()
            {
                destoryed = true;
            
                if (!mDestroyMethodGot)
                {
                    mDestroyMethod = instance.Type.GetMethod("OnDestroy", 0);
                    mDestroyMethodGot = true;
                }
            
                if (mDestroyMethod != null)
                {
                    appdomain.Invoke(mDestroyMethod, instance, param0);
                }
            }
            
            IMethod mOnTriggerEnterMethod;
            bool mOnTriggerEnterMethodGot;
            void OnTriggerEnter(Collider other)
            {
                if (!mOnTriggerEnterMethodGot)
                {
                    mOnTriggerEnterMethod = instance.Type.GetMethod("OnTriggerEnter", 1);
                    mOnTriggerEnterMethodGot = true;
                }
            
                if (mOnTriggerEnterMethod != null)
                {
                    appdomain.Invoke(mOnTriggerEnterMethod, instance, other);
                }
            }
            
            IMethod mOnTriggerStayMethod;
            bool mOnTriggerStayMethodGot;
            void OnTriggerStay(Collider other)
            {
                if (!mOnTriggerStayMethodGot)
                {
                    mOnTriggerStayMethod = instance.Type.GetMethod("OnTriggerStay", 1);
                    mOnTriggerStayMethodGot = true;
                }
            
                if (mOnTriggerStayMethod != null)
                {
                    appdomain.Invoke(mOnTriggerStayMethod, instance, other);
                }
            }
            
            IMethod mOnTriggerExitMethod;
            bool mOnTriggerExitMethodGot;
            void OnTriggerExit(Collider other)
            {
                if (!mOnTriggerExitMethodGot)
                {
                    mOnTriggerExitMethod = instance.Type.GetMethod("OnTriggerExit", 1);
                    mOnTriggerExitMethodGot = true;
                }
            
                if (mOnTriggerExitMethod != null)
                {
                    appdomain.Invoke(mOnTriggerExitMethod, instance, other);
                }
            }
            
            IMethod mOnCollisionEnterMethod;
            bool mOnCollisionEnterMethodGot;
            void OnCollisionEnter(Collision other)
            {
                if (!mOnCollisionEnterMethodGot)
                {
                    mOnCollisionEnterMethod = instance.Type.GetMethod("OnCollisionEnter", 1);
                    mOnCollisionEnterMethodGot = true;
                }
            
                if (mOnCollisionEnterMethod != null)
                {
                    appdomain.Invoke(mOnCollisionEnterMethod, instance, other);
                }
            }
            
            IMethod mOnCollisionStayMethod;
            bool mOnCollisionStayMethodGot;
            void OnCollisionStay(Collision other)
            {
                if (!mOnCollisionStayMethodGot)
                {
                    mOnCollisionStayMethod = instance.Type.GetMethod("OnCollisionStay", 1);
                    mOnCollisionStayMethodGot = true;
                }
            
                if (mOnCollisionStayMethod != null)
                {
                    appdomain.Invoke(mOnCollisionStayMethod, instance, other);
                }
            }
            
            IMethod mOnCollisionExitMethod;
            bool mOnCollisionExitMethodGot;
            void OnCollisionExit(Collision other)
            {
                if (!mOnCollisionExitMethodGot)
                {
                    mOnCollisionExitMethod = instance.Type.GetMethod("OnCollisionExit", 1);
                    mOnCollisionExitMethodGot = true;
                }
            
                if (mOnCollisionExitMethod != null)
                {
                    appdomain.Invoke(mOnCollisionExitMethod, instance, other);
                }
            }
                        
                        
            IMethod mOnValidateMethod;
            bool mOnValidateMethodGot;
            void OnValidate()
            {
                if (instance != null)
                {
                    if (!mOnValidateMethodGot)
                    {
                        mOnValidateMethod = instance.Type.GetMethod("OnValidate", 0);
                        mOnValidateMethodGot = true;
                    }
            
                    if (mOnValidateMethod != null)
                    {
                        appdomain.Invoke(mOnValidateMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnAnimatorMoveMethod;
            bool mOnAnimatorMoveMethodGot;
            void OnAnimatorMove()
            {
                if (instance != null)
                {
                    if (!mOnAnimatorMoveMethodGot)
                    {
                        mOnAnimatorMoveMethod = instance.Type.GetMethod("OnAnimatorMove", 0);
                        mOnAnimatorMoveMethodGot = true;
                    }
            
                    if (mOnAnimatorMoveMethod != null)
                    {
                        appdomain.Invoke(mOnAnimatorMoveMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnApplicationFocusMethod;
            bool mOnApplicationFocusMethodGot;
            void OnApplicationFocus(bool hasFocus)
            {
                if (instance != null)
                {
                    if (!mOnApplicationFocusMethodGot)
                    {
                        mOnApplicationFocusMethod = instance.Type.GetMethod("OnApplicationFocus", 1);
                        mOnApplicationFocusMethodGot = true;
                    }
            
                    if (mOnApplicationFocusMethod != null)
                    {
                        appdomain.Invoke(mOnApplicationFocusMethod, instance, hasFocus);
                    }
                }
            }
            
            IMethod mOnApplicationPauseMethod;
            bool mOnApplicationPauseMethodGot;
            void OnApplicationPause(bool pauseStatus)
            {
                if (instance != null)
                {
                    if (!mOnApplicationPauseMethodGot)
                    {
                        mOnApplicationPauseMethod = instance.Type.GetMethod("OnApplicationPause", 1);
                        mOnApplicationPauseMethodGot = true;
                    }
            
                    if (mOnApplicationPauseMethod != null)
                    {
                        appdomain.Invoke(mOnApplicationPauseMethod, instance, pauseStatus);
                    }
                }
            }
            
            IMethod mOnApplicationQuitMethod;
            bool mOnApplicationQuitMethodGot;
            void OnApplicationQuit()
            {
                if (instance != null)
                {
                    if (!mOnApplicationQuitMethodGot)
                    {
                        mOnApplicationQuitMethod = instance.Type.GetMethod("OnApplicationQuit", 0);
                        mOnApplicationQuitMethodGot = true;
                    }
            
                    if (mOnApplicationQuitMethod != null)
                    {
                        appdomain.Invoke(mOnApplicationQuitMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnBecameInvisibleMethod;
            bool mOnBecameInvisibleMethodGot;
            void OnBecameInvisible()
            {
                if (instance != null)
                {
                    if (!mOnBecameInvisibleMethodGot)
                    {
                        mOnBecameInvisibleMethod = instance.Type.GetMethod("OnBecameInvisible", 0);
                        mOnBecameInvisibleMethodGot = true;
                    }
            
                    if (mOnBecameInvisibleMethod != null)
                    {
                        appdomain.Invoke(mOnBecameInvisibleMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnBecameVisibleMethod;
            bool mOnBecameVisibleMethodGot;
            void OnBecameVisible()
            {
                if (instance != null)
                {
                    if (!mOnBecameVisibleMethodGot)
                    {
                        mOnBecameVisibleMethod = instance.Type.GetMethod("OnBecameVisible", 0);
                        mOnBecameVisibleMethodGot = true;
                    }
            
                    if (mOnBecameVisibleMethod != null)
                    {
                        appdomain.Invoke(mOnBecameVisibleMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnDrawGizmosMethod;
            bool mOnDrawGizmosMethodGot;
            void OnDrawGizmos()
            {
                if (instance != null)
                {
                    if (!mOnDrawGizmosMethodGot)
                    {
                        mOnDrawGizmosMethod = instance.Type.GetMethod("OnDrawGizmos", 0);
                        mOnDrawGizmosMethodGot = true;
                    }
            
                    if (mOnDrawGizmosMethod != null)
                    {
                        appdomain.Invoke(mOnDrawGizmosMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnJointBreakMethod;
            bool mOnJointBreakMethodGot;
            void OnJointBreak(float breakForce)
            {
                if (instance != null)
                {
                    if (!mOnJointBreakMethodGot)
                    {
                        mOnJointBreakMethod = instance.Type.GetMethod("OnJointBreak", 1);
                        mOnJointBreakMethodGot = true;
                    }
            
                    if (mOnJointBreakMethod != null)
                    {
                        appdomain.Invoke(mOnJointBreakMethod, instance, breakForce);
                    }
                }
            }
            
            IMethod mOnMouseDownMethod;
            bool mOnMouseDownMethodGot;
            void OnMouseDown()
            {
                if (instance != null)
                {
                    if (!mOnMouseDownMethodGot)
                    {
                        mOnMouseDownMethod = instance.Type.GetMethod("OnMouseDown", 0);
                        mOnMouseDownMethodGot = true;
                    }
            
                    if (mOnMouseDownMethod != null)
                    {
                        appdomain.Invoke(mOnMouseDownMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnMouseDragMethod;
            bool mOnMouseDragMethodGot;
            void OnMouseDrag()
            {
                if (instance != null)
                {
                    if (!mOnMouseDragMethodGot)
                    {
                        mOnMouseDragMethod = instance.Type.GetMethod("OnMouseDrag", 0);
                        mOnMouseDragMethodGot = true;
                    }
            
                    if (mOnMouseDragMethod != null)
                    {
                        appdomain.Invoke(mOnMouseDragMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnMouseEnterMethod;
            bool mOnMouseEnterMethodGot;
            void OnMouseEnter()
            {
                if (instance != null)
                {
                    if (!mOnMouseEnterMethodGot)
                    {
                        mOnMouseEnterMethod = instance.Type.GetMethod("OnMouseEnter", 0);
                        mOnMouseEnterMethodGot = true;
                    }
            
                    if (mOnMouseEnterMethod != null)
                    {
                        appdomain.Invoke(mOnMouseEnterMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnMouseExitMethod;
            bool mOnMouseExitMethodGot;
            void OnMouseExit()
            {
                if (instance != null)
                {
                    if (!mOnMouseExitMethodGot)
                    {
                        mOnMouseExitMethod = instance.Type.GetMethod("OnMouseExit", 0);
                        mOnMouseExitMethodGot = true;
                    }
            
                    if (mOnMouseExitMethod != null)
                    {
                        appdomain.Invoke(mOnMouseExitMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnMouseOverMethod;
            bool mOnMouseOverMethodGot;
            void OnMouseOver()
            {
                if (instance != null)
                {
                    if (!mOnMouseOverMethodGot)
                    {
                        mOnMouseOverMethod = instance.Type.GetMethod("OnMouseOver", 0);
                        mOnMouseOverMethodGot = true;
                    }
            
                    if (mOnMouseOverMethod != null)
                    {
                        appdomain.Invoke(mOnMouseOverMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnMouseUpMethod;
            bool mOnMouseUpMethodGot;
            void OnMouseUp()
            {
                if (instance != null)
                {
                    if (!mOnMouseUpMethodGot)
                    {
                        mOnMouseUpMethod = instance.Type.GetMethod("OnMouseUp", 0);
                        mOnMouseUpMethodGot = true;
                    }
            
                    if (mOnMouseUpMethod != null)
                    {
                        appdomain.Invoke(mOnMouseUpMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnParticleCollisionMethod;
            bool mOnParticleCollisionMethodGot;
            void OnParticleCollision(GameObject other)
            {
                if (instance != null)
                {
                    if (!mOnParticleCollisionMethodGot)
                    {
                        mOnParticleCollisionMethod = instance.Type.GetMethod("OnParticleCollision", 1);
                        mOnParticleCollisionMethodGot = true;
                    }
            
                    if (mOnParticleCollisionMethod != null)
                    {
                        appdomain.Invoke(mOnParticleCollisionMethod, instance, other);
                    }
                }
            }
            
            IMethod mOnParticleTriggerMethod;
            bool mOnParticleTriggerMethodGot;
            void OnParticleTrigger()
            {
                if (instance != null)
                {
                    if (!mOnParticleTriggerMethodGot)
                    {
                        mOnParticleTriggerMethod = instance.Type.GetMethod("OnParticleTrigger", 0);
                        mOnParticleTriggerMethodGot = true;
                    }
            
                    if (mOnParticleTriggerMethod != null)
                    {
                        appdomain.Invoke(mOnParticleTriggerMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnPostRenderMethod;
            bool mOnPostRenderMethodGot;
            void OnPostRender()
            {
                if (instance != null)
                {
                    if (!mOnPostRenderMethodGot)
                    {
                        mOnPostRenderMethod = instance.Type.GetMethod("OnPostRender", 0);
                        mOnPostRenderMethodGot = true;
                    }
            
                    if (mOnPostRenderMethod != null)
                    {
                        appdomain.Invoke(mOnPostRenderMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnPreCullMethod;
            bool mOnPreCullMethodGot;
            void OnPreCull()
            {
                if (instance != null)
                {
                    if (!mOnPreCullMethodGot)
                    {
                        mOnPreCullMethod = instance.Type.GetMethod("OnPreCull", 0);
                        mOnPreCullMethodGot = true;
                    }
            
                    if (mOnPreCullMethod != null)
                    {
                        appdomain.Invoke(mOnPreCullMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnPreRenderMethod;
            bool mOnPreRenderMethodGot;
            void OnPreRender()
            {
                if (instance != null)
                {
                    if (!mOnPreRenderMethodGot)
                    {
                        mOnPreRenderMethod = instance.Type.GetMethod("OnPreRender", 0);
                        mOnPreRenderMethodGot = true;
                    }
            
                    if (mOnPreRenderMethod != null)
                    {
                        appdomain.Invoke(mOnPreRenderMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnRenderImageMethod;
            bool mOnRenderImageMethodGot;
            void OnRenderImage(RenderTexture src, RenderTexture dest)
            {
                if (instance != null)
                {
                    if (!mOnRenderImageMethodGot)
                    {
                        mOnRenderImageMethod = instance.Type.GetMethod("OnRenderImage", 2);
                        mOnRenderImageMethodGot = true;
                    }
            
                    if (mOnRenderImageMethod != null)
                    {
                        appdomain.Invoke(mOnRenderImageMethod, instance, src, dest);
                    }
                }
            }
            
            IMethod mOnRenderObjectMethod;
            bool mOnRenderObjectMethodGot;
            void OnRenderObject()
            {
                if (instance != null)
                {
                    if (!mOnRenderObjectMethodGot)
                    {
                        mOnRenderObjectMethod = instance.Type.GetMethod("OnRenderObject", 0);
                        mOnRenderObjectMethodGot = true;
                    }
            
                    if (mOnRenderObjectMethod != null)
                    {
                        appdomain.Invoke(mOnRenderObjectMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnServerInitializedMethod;
            bool mOnServerInitializedMethodGot;
            void OnServerInitialized()
            {
                if (instance != null)
                {
                    if (!mOnServerInitializedMethodGot)
                    {
                        mOnServerInitializedMethod = instance.Type.GetMethod("OnServerInitialized", 0);
                        mOnServerInitializedMethodGot = true;
                    }
            
                    if (mOnServerInitializedMethod != null)
                    {
                        appdomain.Invoke(mOnServerInitializedMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnAnimatorIKMethod;
            bool mOnAnimatorIKMethodGot;
            void OnAnimatorIK(int layerIndex)
            {
                if (instance != null)
                {
                    if (!mOnAnimatorIKMethodGot)
                    {
                        mOnAnimatorIKMethod = instance.Type.GetMethod("OnAnimatorIK", 1);
                        mOnAnimatorIKMethodGot = true;
                    }
            
                    if (mOnAnimatorIKMethod != null)
                    {
                        appdomain.Invoke(mOnAnimatorIKMethod, instance, layerIndex);
                    }
                }
            }
            
            IMethod mOnAudioFilterReadMethod;
            bool mOnAudioFilterReadMethodGot;
            void OnAudioFilterRead(float[] data, int channels)
            {
                if (instance != null)
                {
                    if (!mOnAudioFilterReadMethodGot)
                    {
                        mOnAudioFilterReadMethod = instance.Type.GetMethod("OnAudioFilterRead", 2);
                        mOnAudioFilterReadMethodGot = true;
                    }
            
                    if (mOnAudioFilterReadMethod != null)
                    {
                        appdomain.Invoke(mOnAudioFilterReadMethod, instance, data, channels);
                    }
                }
            }
            
            
            IMethod mOnCanvasGroupChangedMethod;
            bool mOnCanvasGroupChangedMethodGot;
            void OnCanvasGroupChanged()
            {
                if (instance != null)
                {
                    if (!mOnCanvasGroupChangedMethodGot)
                    {
                        mOnCanvasGroupChangedMethod = instance.Type.GetMethod("OnCanvasGroupChanged", 0);
                        mOnCanvasGroupChangedMethodGot = true;
                    }
            
                    if (mOnCanvasGroupChangedMethod != null)
                    {
                        appdomain.Invoke(mOnCanvasGroupChangedMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnCanvasHierarchyChangedMethod;
            bool mOnCanvasHierarchyChangedMethodGot;
            void OnCanvasHierarchyChanged()
            {
                if (instance != null)
                {
                    if (!mOnCanvasHierarchyChangedMethodGot)
                    {
                        mOnCanvasHierarchyChangedMethod = instance.Type.GetMethod("OnCanvasHierarchyChanged", 0);
                        mOnCanvasHierarchyChangedMethodGot = true;
                    }
            
                    if (mOnCanvasHierarchyChangedMethod != null)
                    {
                        appdomain.Invoke(mOnCanvasHierarchyChangedMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnCollisionEnter2DMethod;
            bool mOnCollisionEnter2DMethodGot;
            void OnCollisionEnter2D(Collision2D other)
            {
                if (!mOnCollisionEnter2DMethodGot)
                {
                    mOnCollisionEnter2DMethod = instance.Type.GetMethod("OnCollisionEnter2D", 1);
                    mOnCollisionEnter2DMethodGot = true;
                }
            
                if (mOnCollisionEnter2DMethod != null)
                {
                    appdomain.Invoke(mOnCollisionEnter2DMethod, instance, other);
                }
            }
            
            IMethod mOnCollisionExit2DMethod;
            bool mOnCollisionExit2DMethodGot;
            void OnCollisionExit2D(Collision2D other)
            {
                if (!mOnCollisionExit2DMethodGot)
                {
                    mOnCollisionExit2DMethod = instance.Type.GetMethod("OnCollisionExit2D", 1);
                    mOnCollisionExit2DMethodGot = true;
                }
            
                if (mOnCollisionExit2DMethod != null)
                {
                    appdomain.Invoke(mOnCollisionExit2DMethod, instance, other);
                }
            }
            
            IMethod mOnCollisionStay2DMethod;
            bool mOnCollisionStay2DMethodGot;
            void OnCollisionStay2D(Collision2D other)
            {
                if (!mOnCollisionStay2DMethodGot)
                {
                    mOnCollisionStay2DMethod = instance.Type.GetMethod("OnCollisionStay2D", 1);
                    mOnCollisionStay2DMethodGot = true;
                }
            
                if (mOnCollisionStay2DMethod != null)
                {
                    appdomain.Invoke(mOnCollisionStay2DMethod, instance, other);
                }
            }
            
            IMethod mOnConnectedToServerMethod;
            bool mOnConnectedToServerMethodGot;
            void OnConnectedToServer()
            {
                if (instance != null)
                {
                    if (!mOnConnectedToServerMethodGot)
                    {
                        mOnConnectedToServerMethod = instance.Type.GetMethod("OnConnectedToServer", 0);
                        mOnConnectedToServerMethodGot = true;
                    }
            
                    if (mOnConnectedToServerMethod != null)
                    {
                        appdomain.Invoke(mOnConnectedToServerMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnControllerColliderHitMethod;
            bool mOnControllerColliderHitMethodGot;
            void OnControllerColliderHit(ControllerColliderHit hit)
            {
                if (instance != null)
                {
                    if (!mOnControllerColliderHitMethodGot)
                    {
                        mOnControllerColliderHitMethod = instance.Type.GetMethod("OnControllerColliderHit", 1);
                        mOnControllerColliderHitMethodGot = true;
                    }
            
                    if (mOnControllerColliderHitMethod != null)
                    {
                        appdomain.Invoke(mOnControllerColliderHitMethod, instance, hit);
                    }
                }
            }
            
            IMethod mOnDrawGizmosSelectedMethod;
            bool mOnDrawGizmosSelectedMethodGot;
            void OnDrawGizmosSelected()
            {
                if (instance != null)
                {
                    if (!mOnDrawGizmosSelectedMethodGot)
                    {
                        mOnDrawGizmosSelectedMethod = instance.Type.GetMethod("OnDrawGizmosSelected", 0);
                        mOnDrawGizmosSelectedMethodGot = true;
                    }
            
                    if (mOnDrawGizmosSelectedMethod != null)
                    {
                        appdomain.Invoke(mOnDrawGizmosSelectedMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnGUIMethod;
            bool mOnGUIMethodGot;
            void OnGUI()
            {
                if (instance != null)
                {
                    if (!mOnGUIMethodGot)
                    {
                        mOnGUIMethod = instance.Type.GetMethod("OnGUI", 0);
                        mOnGUIMethodGot = true;
                    }
            
                    if (mOnGUIMethod != null)
                    {
                        appdomain.Invoke(mOnGUIMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnJointBreak2DMethod;
            bool mOnJointBreak2DMethodGot;
            void OnJointBreak2D(Joint2D brokenJoint)
            {
                if (instance != null)
                {
                    if (!mOnJointBreak2DMethodGot)
                    {
                        mOnJointBreak2DMethod = instance.Type.GetMethod("OnJointBreak2D", 1);
                        mOnJointBreak2DMethodGot = true;
                    }
            
                    if (mOnJointBreak2DMethod != null)
                    {
                        appdomain.Invoke(mOnJointBreak2DMethod, instance, brokenJoint);
                    }
                }
            }
            
            IMethod mOnParticleSystemStoppedMethod;
            bool mOnParticleSystemStoppedMethodGot;
            void OnParticleSystemStopped()
            {
                if (instance != null)
                {
                    if (!mOnParticleSystemStoppedMethodGot)
                    {
                        mOnParticleSystemStoppedMethod = instance.Type.GetMethod("OnParticleSystemStopped", 0);
                        mOnParticleSystemStoppedMethodGot = true;
                    }
            
                    if (mOnParticleSystemStoppedMethod != null)
                    {
                        appdomain.Invoke(mOnParticleSystemStoppedMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnTransformChildrenChangedMethod;
            bool mOnTransformChildrenChangedMethodGot;
            void OnTransformChildrenChanged()
            {
                if (instance != null)
                {
                    if (!mOnTransformChildrenChangedMethodGot)
                    {
                        mOnTransformChildrenChangedMethod = instance.Type.GetMethod("OnTransformChildrenChanged", 0);
                        mOnTransformChildrenChangedMethodGot = true;
                    }
            
                    if (mOnTransformChildrenChangedMethod != null)
                    {
                        appdomain.Invoke(mOnTransformChildrenChangedMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnTransformParentChangedMethod;
            bool mOnTransformParentChangedMethodGot;
            void OnTransformParentChanged()
            {
                if (instance != null)
                {
                    if (!mOnTransformParentChangedMethodGot)
                    {
                        mOnTransformParentChangedMethod = instance.Type.GetMethod("OnTransformParentChanged", 0);
                        mOnTransformParentChangedMethodGot = true;
                    }
            
                    if (mOnTransformParentChangedMethod != null)
                    {
                        appdomain.Invoke(mOnTransformParentChangedMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnTriggerEnter2DMethod;
            bool mOnTriggerEnter2DMethodGot;
            void OnTriggerEnter2D(Collider2D other)
            {
                if (instance != null)
                {
                    if (!mOnTriggerEnter2DMethodGot)
                    {
                        mOnTriggerEnter2DMethod = instance.Type.GetMethod("OnTriggerEnter2D", 1);
                        mOnTriggerEnter2DMethodGot = true;
                    }
            
                    if (mOnTriggerEnter2DMethod != null)
                    {
                        appdomain.Invoke(mOnTriggerEnter2DMethod, instance, other);
                    }
                }
            }
            
            IMethod mOnTriggerExit2DMethod;
            bool mOnTriggerExit2DMethodGot;
            void OnTriggerExit2D(Collider2D other)
            {
                if (instance != null)
                {
                    if (!mOnTriggerExit2DMethodGot)
                    {
                        mOnTriggerExit2DMethod = instance.Type.GetMethod("OnTriggerExit2D", 1);
                        mOnTriggerExit2DMethodGot = true;
                    }
            
                    if (mOnTriggerExit2DMethod != null)
                    {
                        appdomain.Invoke(mOnTriggerExit2DMethod, instance, other);
                    }
                }
            }
            
            IMethod mOnTriggerStay2DMethod;
            bool mOnTriggerStay2DMethodGot;
            void OnTriggerStay2D(Collider2D other)
            {
                if (instance != null)
                {
                    if (!mOnTriggerStay2DMethodGot)
                    {
                        mOnTriggerStay2DMethod = instance.Type.GetMethod("OnTriggerStay2D", 1);
                        mOnTriggerStay2DMethodGot = true;
                    }
            
                    if (mOnTriggerStay2DMethod != null)
                    {
                        appdomain.Invoke(mOnTriggerStay2DMethod, instance, other);
                    }
                }
            }
            
            IMethod mOnWillRenderObjectMethod;
            bool mOnWillRenderObjectMethodGot;
            void OnWillRenderObject()
            {
                if (instance != null)
                {
                    if (!mOnWillRenderObjectMethodGot)
                    {
                        mOnWillRenderObjectMethod = instance.Type.GetMethod("OnWillRenderObject", 0);
                        mOnWillRenderObjectMethodGot = true;
                    }
            
                    if (mOnWillRenderObjectMethod != null)
                    {
                        appdomain.Invoke(mOnWillRenderObjectMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnBeforeTransformParentChangedMethod;
            bool mOnBeforeTransformParentChangedMethodGot;
            void OnBeforeTransformParentChanged()
            {
                if (instance != null)
                {
                    if (!mOnBeforeTransformParentChangedMethodGot)
                    {
                        mOnBeforeTransformParentChangedMethod =
                            instance.Type.GetMethod("OnBeforeTransformParentChanged", 0);
                        mOnBeforeTransformParentChangedMethodGot = true;
                    }
            
                    if (mOnBeforeTransformParentChangedMethod != null)
                    {
                        appdomain.Invoke(mOnBeforeTransformParentChangedMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnDidApplyAnimationPropertiesMethod;
            bool mOnDidApplyAnimationPropertiesMethodGot;
            void OnDidApplyAnimationProperties()
            {
                if (instance != null)
                {
                    if (!mOnDidApplyAnimationPropertiesMethodGot)
                    {
                        mOnDidApplyAnimationPropertiesMethod = instance.Type.GetMethod("OnDidApplyAnimationProperties", 0);
                        mOnDidApplyAnimationPropertiesMethodGot = true;
                    }
            
                    if (mOnDidApplyAnimationPropertiesMethod != null)
                    {
                        appdomain.Invoke(mOnDidApplyAnimationPropertiesMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnMouseUpAsButtonMethod;
            bool mOnMouseUpAsButtonMethodGot;
            void OnMouseUpAsButton()
            {
                if (instance != null)
                {
                    if (!mOnMouseUpAsButtonMethodGot)
                    {
                        mOnMouseUpAsButtonMethod = instance.Type.GetMethod("OnMouseUpAsButton", 0);
                        mOnMouseUpAsButtonMethodGot = true;
                    }
            
                    if (mOnMouseUpAsButtonMethod != null)
                    {
                        appdomain.Invoke(mOnMouseUpAsButtonMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnParticleUpdateJobScheduledMethod;
            bool mOnParticleUpdateJobScheduledMethodGot;
            void OnParticleUpdateJobScheduled()
            {
                if (instance != null)
                {
                    if (!mOnParticleUpdateJobScheduledMethodGot)
                    {
                        mOnParticleUpdateJobScheduledMethod = instance.Type.GetMethod("OnParticleUpdateJobScheduled", 0);
                        mOnParticleUpdateJobScheduledMethodGot = true;
                    }
            
                    if (mOnParticleUpdateJobScheduledMethod != null)
                    {
                        appdomain.Invoke(mOnParticleUpdateJobScheduledMethod, instance, param0);
                    }
                }
            }
            
            IMethod mOnRectTransformDimensionsChangeMethod;
            bool mOnRectTransformDimensionsChangeMethodGot;
            void OnRectTransformDimensionsChange()
            {
                if (instance != null)
                {
                    if (!mOnRectTransformDimensionsChangeMethodGot)
                    {
                        mOnRectTransformDimensionsChangeMethod =
                            instance.Type.GetMethod("OnRectTransformDimensionsChange", 0);
                        mOnRectTransformDimensionsChangeMethodGot = true;
                    }
            
                    if (mOnRectTransformDimensionsChangeMethod != null)
                    {
                        appdomain.Invoke(mOnRectTransformDimensionsChangeMethod, instance, param0);
                    }
                }
            }
            
            IMethod mToStringMethod;
            bool mToStringMethodGot;
            public override string ToString()
            {
                if (instance != null)
                {
                    if (!mToStringMethodGot)
                    {
                        mToStringMethod =
                            instance.Type.GetMethod("ToString", 0);
                        mToStringMethodGot = true;
                    }
    
                    if (mToStringMethod != null)
                    {
                        appdomain.Invoke(mToStringMethod, instance, param0);
                    }
                }
    
                return instance?.Type?.FullName ?? base.ToString();
            }
            
            #endregion
        }
    }
}

