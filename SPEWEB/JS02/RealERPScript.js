function RealERPScript() {


    this.GetModule = function (ModuleId, InputName) {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: 'Service/UserService.asmx/GetModule',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'ModuleId': ModuleId, 'InputName': InputName }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };
    this.GetShortCut = function () {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: 'Service/UserService.asmx/GetShortCut',          
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };

    this.GetCompInf = function (date) {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: '../../Service/UserService.asmx/GetCompInf',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'date': date }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };

    this.GetNotifications = function (userid, url) {
        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: url,
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'userid': userid }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    console.log(res.responseText);
                    alert(res.responseText);
                }
            }
        });



        return results;



    };
 }