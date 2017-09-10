"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var i18next = require("i18next");
var LanguageDetector = require("i18next-browser-languagedetector");
i18next
    .use(LanguageDetector)
    .init({
    resources: {
        en: {
            translations: {}
        }
    },
    fallbackLng: "en",
    ns: ["translations"],
    defaultNS: "translations",
    keySeparator: false,
    interpolation: {
        escapeValue: false,
        formatSeparator: ","
    },
    react: {
        wait: true
    }
}, undefined);
exports.default = i18next;
//# sourceMappingURL=i18n.js.map