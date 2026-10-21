import type { Config } from 'tailwindcss';

import path from 'node:path';

import { addDynamicIconSelectors } from '@iconify/tailwind';
import { getPackagesSync } from '@manypkg/get-packages';
import typographyPlugin from '@tailwindcss/typography';
import animate from 'tailwindcss-animate';

import { enterAnimationPlugin } from './plugins/entry';

// import defaultTheme from 'tailwindcss/defaultTheme';

const { packages } = getPackagesSync(process.cwd());

const tailwindPackages: string[] = [];

packages.forEach((pkg) => {
  // The apps directory and the @vben-core/tailwind-ui package need to use tailwindcss ui
  // if (fs.existsSync(path.join(pkg.dir, 'tailwind.config.mjs'))) {
  tailwindPackages.push(pkg.dir);
  // }
});

const shadcnUiColors = {
  accent: {
    DEFAULT: 'hsl(var(--accent))',
    foreground: 'hsl(var(--accent-foreground))',
    hover: 'hsl(var(--accent-hover))',
    lighter: 'has(val(--accent-lighter))',
  },
  background: {
    deep: 'hsl(var(--background-deep))',
    DEFAULT: 'hsl(var(--background))',
  },
  border: {
    DEFAULT: 'hsl(var(--border))',
  },
  card: {
    DEFAULT: 'hsl(var(--card))',
    foreground: 'hsl(var(--card-foreground))',
  },
  destructive: {
    ...createColorsPalette('destructive'),
    DEFAULT: 'hsl(var(--destructive))',
  },

  foreground: {
    DEFAULT: 'hsl(var(--foreground))',
  },

  input: {
    background: 'hsl(var(--input-background))',
    DEFAULT: 'hsl(var(--input))',
  },
  muted: {
    DEFAULT: 'hsl(var(--muted))',
    foreground: 'hsl(var(--muted-foreground))',
  },
  popover: {
    DEFAULT: 'hsl(var(--popover))',
    foreground: 'hsl(var(--popover-foreground))',
  },
  primary: {
    ...createColorsPalette('primary'),
    DEFAULT: 'hsl(var(--primary))',
  },

  ring: 'hsl(var(--ring))',
  secondary: {
    DEFAULT: 'hsl(var(--secondary))',
    desc: 'hsl(var(--secondary-desc))',
    foreground: 'hsl(var(--secondary-foreground))',
  },
};

const customColors = {
  green: {
    ...createColorsPalette('green'),
    foreground: 'hsl(var(--success-foreground))',
  },
  header: {
    DEFAULT: 'hsl(var(--header))',
  },
  heavy: {
    DEFAULT: 'hsl(var(--heavy))',
    foreground: 'hsl(var(--heavy-foreground))',
  },
  main: {
    DEFAULT: 'hsl(var(--main))',
  },
  overlay: {
    content: 'hsl(var(--overlay-content))',
    DEFAULT: 'hsl(var(--overlay))',
  },
  red: {
    ...createColorsPalette('red'),
    foreground: 'hsl(var(--destructive-foreground))',
  },
  sidebar: {
    deep: 'hsl(var(--sidebar-deep))',
    DEFAULT: 'hsl(var(--sidebar))',
  },
  success: {
    ...createColorsPalette('success'),
    DEFAULT: 'hsl(var(--success))',
  },
  warning: {
    ...createColorsPalette('warning'),
    DEFAULT: 'hsl(var(--warning))',
  },
  yellow: {
    ...createColorsPalette('yellow'),
    foreground: 'hsl(var(--warning-foreground))',
  },
};

export default {
  content: [
    './index.html',
    ...tailwindPackages.map((item) =>
      path.join(item, 'src/**/*.{vue,js,ts,jsx,tsx,svelte,astro,html}'),
    ),
  ],
  darkMode: 'selector',
  plugins: [
    animate,
    typographyPlugin,
    addDynamicIconSelectors(),
    enterAnimationPlugin,
  ],
  prefix: '',
  theme: {
    container: {
      center: true,
      padding: '2rem',
      screens: {
        '2xl': '1400px',
      },
    },
    extend: {
      animation: {
        'accordion-down': 'accordion-down 0.2s ease-out',
        'accordion-up': 'accordion-up 0.2s ease-out',
        'collapsible-down': 'collapsible-down 0.2s ease-in-out',
        'collapsible-up': 'collapsible-up 0.2s ease-in-out',
        float: 'float 5s linear 0ms infinite',
      },

      animationDuration: {
        '2000': '2000ms',
        '3000': '3000ms',
      },
      borderRadius: {
        lg: 'var(--radius)',
        md: 'calc(var(--radius) - 2px)',
        sm: 'calc(var(--radius) - 4px)',
        xl: 'calc(var(--radius) + 4px)',
      },
      boxShadow: {
        float: `0 6px 16px 0 rgb(0 0 0 / 8%),
          0 3px 6px -4px rgb(0 0 0 / 12%),
          0 9px 28px 8px rgb(0 0 0 / 5%)`,
      },
      colors: {
        ...customColors,
        ...shadcnUiColors,
      },
      fontFamily: {
        sans: [
          'var(--font-family)',
          //  ...defaultTheme.fontFamily.sans
        ],
      },
      keyframes: {
        'accordion-down': {
          from: { height: '0' },
          to: { height: 'var(--reka-accordion-content-height)' },
        },
        'accordion-up': {
          from: { height: 'var(--reka-accordion-content-height)' },
          to: { height: '0' },
        },
        'collapsible-down': {
          from: { height: '0' },
          to: { height: 'var(--reka-collapsible-content-height)' },
        },
        'collapsible-up': {
          from: { height: 'var(--reka-collapsible-content-height)' },
          to: { height: '0' },
        },
        float: {
          '0%': { transform: 'translateY(0)' },
          '50%': { transform: 'translateY(-20px)' },
          '100%': { transform: 'translateY(0)' },
        },
      },
      zIndex: {
        '100': '100',
        '1000': '1000',
      },
    },
  },
  safelist: ['dark'],
} as Config;

function createColorsPalette(name: string) {
  // backgroundLightest: '#EFF6FF', // Tailwind CSS default `blue-50`
  // backgroundLighter: '#DBEAFE', // Tailwind CSS default `blue-100`
  // backgroundLight: '#BFDBFE', // Tailwind CSS default `blue-200`
  // borderLight: '#93C5FD', // Tailwind CSS default `blue-300`
  // border: '#60A5FA', // Tailwind CSS default `blue-400`
  // main: '#3B82F6', // Tailwind CSS default `blue-500`
  // hover: '#2563EB', // Tailwind CSS default `blue-600`
  // active: '#1D4ED8', // Tailwind CSS default `blue-700`
  // backgroundDark: '#1E40AF', // Tailwind CSS default `blue-800`
  // backgroundDarker: '#1E3A8A', // Tailwind CSS default `blue-900`
  // backgroundDarkest: '#172554', // Tailwind CSS default `blue-950`

  // • backgroundLightest (#EFF6FF): Suitable for the lightest background color, possibly for very slight shadows or the background of a card.
  // • backgroundLighter (#DBEAFE): Suitable for a slightly lighter background color, usually used for secondary backgrounds or slightly lighter areas.
  // • backgroundLight (#BFDBFE): Suitable for a light background, which may be used as the background of an input box or form area.
  // • borderLight (#93C5FD): Suitable for light borders, possibly for input boxes or card borders.
  // • border (#60A5FA): Suitable for a normal border, possibly for buttons or cards.
  // • main (#3B82F6): Suitable for the main theme color, usually used for buttons, links or the main accent color.
  // • hover (#2563EB): Applicable to the color when the mouse is hovering, such as the background color or border color when the button is hovered.
  // • active (#1D4ED8): Applicable to the color in the active state, such as the background color or border color when the button is pressed.
  // • backgroundDark (#1E40AF): Suitable for dark backgrounds, possibly for primary buttons or dark card backgrounds.
  // • backgroundDarker (#1E3A8A): Suitable for darker backgrounds, typically used in header navigation bars or footers.
  // • backgroundDarkest (#172554): Suitable for the darkest background, possibly for very dark areas or extreme contrasting colors.

  return {
    50: `hsl(var(--${name}-50))`,
    100: `hsl(var(--${name}-100))`,
    200: `hsl(var(--${name}-200))`,
    300: `hsl(var(--${name}-300))`,
    400: `hsl(var(--${name}-400))`,
    500: `hsl(var(--${name}-500))`,
    600: `hsl(var(--${name}-600))`,
    700: `hsl(var(--${name}-700))`,
    // 800: `hsl(var(--${name}-800))`,
    // 900: `hsl(var(--${name}-900))`,
    // 950: `hsl(var(--${name}-950))`,
    // The color in the activated state, suitable for the background color or border color when the button is pressed.
    active: `hsl(var(--${name}-700))`,
    // Light background, suitable for the background of input boxes or form areas.
    'background-light': `hsl(var(--${name}-200))`,
    // Suitable for a slightly lighter background color, usually used for secondary backgrounds or slightly lighter areas.
    'background-lighter': `hsl(var(--${name}-100))`,
    // The lightest background color, suitable for very slight shadows or backgrounds of cards.
    'background-lightest': `hsl(var(--${name}-50))`,
    // Suitable for normal borders, possibly for buttons or cards.
    border: `hsl(var(--${name}-400))`,
    // Light border, suitable for the border of input box or card.
    'border-light': `hsl(var(--${name}-300))`,
    foreground: `hsl(var(--${name}-foreground))`,
    // The color when the mouse is hovering, suitable for the background color or border color when the button is hovering.
    hover: `hsl(var(--${name}-600))`,
    // Main color text
    text: `hsl(var(--${name}-500))`,
    // Main color text active state
    'text-active': `hsl(var(--${name}-700))`,
    // Main color text floating state
    'text-hover': `hsl(var(--${name}-600))`,
  };
}
