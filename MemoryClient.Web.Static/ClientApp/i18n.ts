import i18next = require("i18next");
import LanguageDetector = require("i18next-browser-languagedetector");

i18next
    .use(LanguageDetector)
    .init({
        resources: {
            en: {
                translations: {
                }
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
    } as Object, undefined);

export default i18next;