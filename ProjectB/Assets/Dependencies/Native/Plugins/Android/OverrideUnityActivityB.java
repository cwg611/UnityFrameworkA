package com.company.product;
import android.os.Bundle;
import android.widget.FrameLayout;

import com.unity3d.player.UnityPlayerActivity;

import org.json.JSONObject;

public abstract class OverrideUnityActivityB extends UnityPlayerActivity
{ 
  public static OverrideUnityActivityB instance = null;

  protected void UnitySendMessage(String gameObj, String method, String arg) { mUnityPlayer.UnitySendMessage(gameObj,method,arg); }

  protected FrameLayout getUnityFrameLayout() { return mUnityPlayer; }

  abstract protected void UnityToNative(String object);

  @Override
  protected void onCreate(Bundle savedInstanceState)
  {
      super.onCreate(savedInstanceState);
      instance = this;
  }

  @Override
  protected void onDestroy() {
      super.onDestroy();
      instance = null;
  }
}
