window.liffExt = {
    init: function (context) {
        liff.init(
            function (data) {
                context.invokeMethod('OnInitSuccess', JSON.stringify(data));
            },
            function (error) {
                context.invokeMethod('OnInitError', JSON.stringify({
                    code: error.code,
                    message: error.message,
                    stack: error.stack
                }));
            }
        );
    },
    showAlert: function (message) {
        alert(message);
    }
};