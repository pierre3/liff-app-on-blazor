window.liffExt = {
    init: function (context) {
        liff.init(
            function (data) {
                context.invokeMethod('LiffInitSuccess', JSON.stringify(data));
            },
            function (error) {
                context.invokeMethod('LiffInitError', JSON.stringify({
                    code: error.code,
                    message: error.message,
                    stack: error.stack
                }));
            }
        );
    }
};