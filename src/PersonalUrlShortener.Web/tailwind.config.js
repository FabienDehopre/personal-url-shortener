/** @type {import('tailwindcss').Config} */

const defaultTheme = require('tailwindcss/defaultTheme');

export default {
  content: [
      './Components/**/*.{razor,html}',
      '../PersonalUrlShortener.Client/**/*.{razor,html}',
  ],
  theme: {
    extend: {
        fontFamily: {
            sans: ['InterVariable', ...defaultTheme.fontFamily.sans],
        },
    },
  },
  plugins: [
      require('@tailwindcss/forms'),
  ],
}

