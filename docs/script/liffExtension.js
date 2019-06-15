window.liffExt = {
    init: function (context) {
        liff.init(
            function (data) {
                context.invokeMethod('LiffInitSuccess', JSON.stringify(data));
            },
            function (error) {
                context.invokeMethod('LiffInitError', JSON.stringify(error));
            }
        );
    }
};