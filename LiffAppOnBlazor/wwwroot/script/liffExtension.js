window.liffExt = {
    init: function (context) {
        liff.init(
            function (data) {
                context.invokeMethod('LiffInitSuccess', data);
            },
            function (error) {
                context.invokeMethod('LiffInitError', { code: error.code, message: error.message, stack: error.stack });
            }
        );
    }
};