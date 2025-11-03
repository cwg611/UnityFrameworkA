#import <Foundation/Foundation.h>
#import "NativeCallProxy.h"


@implementation FrameworkLibAPI

id<NativeCallsProtocol> api = NULL;
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi
{
    api = aApi;
}

@end


extern "C" {
/*
    void UToN_GetLoginToken() {
        return [api UToN_GetLoginToken];
    }

    void UToN_BrowseShop(const char*  str) {
        return [api UToN_BrowseShop:[NSString stringWithUTF8String:str]];
    }

    void UToN_BrowseProductCar(const char*  str) {
        return [api UToN_BrowseProductCar:[NSString stringWithUTF8String:str]];
    }

    void UToN_BrowseProductLocal(const char*  str) {
        return [api UToN_BrowseProductLocal:[NSString stringWithUTF8String:str]];
    }

    void UToN_BrowseProductNormal(const char*  str) {
        return [api UToN_BrowseProductNormal:[NSString stringWithUTF8String:str]];
    }

    void UToN_BackToNative(const char*  str) {
        return [api UToN_BackToNative:[NSString stringWithUTF8String:str]];
    }
*/

    void UnityToNative(const char*  str){
        return [api UnityToNative:[NSString stringWithUTF8String:str]];

    }




}

