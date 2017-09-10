"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_router_dom_1 = require("react-router-dom");
var Layout_1 = require("./components/Layout");
var Home_1 = require("./components/Home");
var FetchData_1 = require("./components/FetchData");
var Counter_1 = require("./components/Counter");
exports.routes = React.createElement(Layout_1.Layout, null,
    React.createElement(react_router_dom_1.Route, { exact: true, path: '/', component: Home_1.default }),
    React.createElement(react_router_dom_1.Route, { path: '/counter', component: Counter_1.default }),
    React.createElement(react_router_dom_1.Route, { path: '/fetchdata/:startDateIndex?', component: FetchData_1.default }));
//# sourceMappingURL=routes.js.map