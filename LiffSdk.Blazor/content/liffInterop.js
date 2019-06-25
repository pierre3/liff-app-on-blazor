window.liffInterop = {
    init: function (dotNetRef) {
        liff.init(
            function (data) {
                dotNetRef.invokeMethod('OnInitSuccess', JSON.stringify(data));
            },
            function (error) {
                dotNetRef.invokeMethod('OnInitError', JSON.stringify({
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