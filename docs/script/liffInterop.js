window.liffInterop = {
    init: function (dotNet) {
        liff.init(
            function (data) {
                dotNet.invokeMethod('OnInitSuccess', JSON.stringify(data));
            },
            function (error) {
                dotNet.invokeMethod('OnInitError', JSON.stringify({
                    code: error.code,
                    message: error.message,
                    stack: error.stack
                }));
            }
        );
    },
    alert: function (message) {
        alert(message);
    }
};