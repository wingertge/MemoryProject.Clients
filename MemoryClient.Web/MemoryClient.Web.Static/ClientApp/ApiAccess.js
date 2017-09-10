"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var $ = require("jquery");
var Cookies = require("js-cookie");
var Api = (function () {
    function Api() {
    }
    Api.post = function (path, data) {
        if (data === void 0) { data = null; }
        return $.ajax(this.apiHost + path, {
            contentType: "application/json; charset=UTF-8",
            crossDomain: true,
            data: data,
            dataType: "json",
            headers: {
                "X-AuthToken": Cookies.get("__auth_token") || ""
            },
            method: "post"
        });
    };
    return Api;
}());
Api.apiHost = "{{API_HOST}}";
exports.default = Api;
//# sourceMappingURL=ApiAccess.js.map