var clientService = {
    post: (url, clientData, successCallback, failedCallback) => {

        $.ajax({
            method: "POST",
            url: url,
            traditional:true,
            data: clientData,
            success: function (data) {
                successCallback(data);
            },
            error: function (err) {
                failedCallback(err);
            }
        
        })
    },
    updateClientStatus : (url,clientId, enableStatus, successCallback,failedCallback) => {
        $.ajax({
            method: "GET",
            url:url,
            success: function (data) {
                successCallback(data)
            },
            error: function (err) {
                failedCallback(err);
            }
        })
    }
}