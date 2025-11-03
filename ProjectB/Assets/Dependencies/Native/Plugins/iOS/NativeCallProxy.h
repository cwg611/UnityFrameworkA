// [!] important set UnityFramework in Target Membership for this file
// [!]           and set Public header visibility

#import <Foundation/Foundation.h>

// NativeCallsProtocol defines protocol with methods you want to be called from managed
@protocol NativeCallsProtocol
@required
/*
- (void)UToN_GetLoginToken;
- (void)UToN_BrowseShop:(NSString *)str;
- (void)UToN_BrowseProductCar:(NSString*)str;
- (void)UToN_BrowseProductLocal:(NSString*)str;
- (void)UToN_BrowseProductNormal:(NSString*)str;
- (void)UToN_BackToNative:(NSString*)str;*/

- (void)UnityToNative:(NSString*)str;

// other methods
@end

__attribute__ ((visibility("default")))
@interface FrameworkLibAPI : NSObject
// call it any time after UnityFrameworkLoad to set object implementing NativeCallsProtocol methods
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi;

@end


