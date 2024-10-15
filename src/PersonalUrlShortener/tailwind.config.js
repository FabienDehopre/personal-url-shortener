/** @type {import('tailwindcss').Config} */
export default {
  content: [
      './Components/**/*.{razor,html}',
      '../PersonalUrlShortener.Client/Pages/**/*.{razor,html}',
  ],
  theme: {
    extend: {},
  },
  plugins: [
      require('@tailwindcss/forms'),
  ],
}

