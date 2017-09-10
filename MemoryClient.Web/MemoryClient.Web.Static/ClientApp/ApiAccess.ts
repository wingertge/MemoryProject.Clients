import * as $ from "jquery";
import * as Cookies from "js-cookie";

class Api {
    private static apiHost = "{{API_HOST}}";

    static post(path: string, data: any = null): Promise<object> {
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
    }
}

export default Api;