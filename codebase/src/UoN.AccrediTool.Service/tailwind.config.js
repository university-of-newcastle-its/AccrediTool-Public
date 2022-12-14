// tailwind.config.js
const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
    future: {
        removeDeprecatedGapUtilities: true,
        purgeLayersByDefault: true
    },
    content: [
        './Views/**/*.cshtml'
    ],
    theme: {
        extend: {
            fontFamily: {
                sans: ['Inter var', ...defaultTheme.fontFamily.sans]
            },
            screens: {
                // dark: { raw: '(prefers-color-scheme: dark)' }
            }
        }
    },
    variants: {},
    plugins: [
    ]
}
