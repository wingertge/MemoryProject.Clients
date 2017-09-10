"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("./css/site.css");
require("bootstrap");
var React = require("react");
var ReactDOM = require("react-dom");
var react_hot_loader_1 = require("react-hot-loader");
var react_redux_1 = require("react-redux");
var react_router_redux_1 = require("react-router-redux");
var history_1 = require("history");
var configureStore_1 = require("./configureStore");
var react_i18next_1 = require("react-i18next");
var i18n_1 = require("./i18n");
var RoutesModule = require("./routes");
var routes = RoutesModule.routes;
// Create browser history to use in the Redux store
var baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
var history = history_1.createBrowserHistory({ basename: baseUrl });
// Get the application-wide store instance, prepopulating with state from the server where available.
var initialState = window.initialReduxState;
var store = configureStore_1.default(history, initialState);
function renderApp() {
    // This code starts up the React app when it runs in a browser. It sets up the routing configuration
    // and injects the app into a DOM element.
    ReactDOM.render(React.createElement(react_hot_loader_1.AppContainer, null,
        React.createElement(react_redux_1.Provider, { store: store },
            React.createElement(react_i18next_1.I18nextProvider, { i18n: i18n_1.default },
                React.createElement(react_router_redux_1.ConnectedRouter, { history: history, children: routes })))), document.getElementById("react-app"));
}
renderApp();
// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept("./routes", function () {
        routes = require("./routes").routes;
        renderApp();
    });
}
//# sourceMappingURL=boot-client.js.map