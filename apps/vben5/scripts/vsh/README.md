# @vben/vsh

A collection of shell scripting tools for the development and management of Vue Vben Admin projects.

## Features

- 🚀 Modern Shell Tools based on Node.js
- 📦 Support modular development and on-demand loading
- 🔍 Provide dependency checking and analysis functions
- 🔄 Support circular dependency scanning
- 📝 Provide package release checking function

## Installation

```bash
# Install using pnpm
pnpm add -D @vben/vsh

# Or use npm
npm install -D @vben/vsh

# Or use yarn
yarn add -D @vben/vsh
```

## How to use

### Global installation

```bash
# Global installation
pnpm add -g @vben/vsh

#Use vsh command
vsh [command]
```

### Local use

```bash
#Add script in package.json
{
  "scripts": {
    "vsh": "vsh"
  }
}

# Run command
pnpm vsh [command]
```

## Command list

- `vsh check-deps`: Check project dependencies
- `vsh scan-circular`: scan for circular dependencies
- `vsh publish-check`: Check package publishing configuration
