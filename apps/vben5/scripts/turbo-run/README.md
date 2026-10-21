# @vben/turbo-run

`turbo-run` is a command line tool that allows you to run commands in multiple packages in parallel. It provides an interactive interface that allows you to select packages to run commands on.

## Features

- 🚀 Interactively select packages to run
- 📦 Support monorepo project structure
- 🔍 Automatically detect available commands
- 🎯 Accurately filter target packets

## Installation

```bash
pnpm add -D @vben/turbo-run
```

## How to use

Basic syntax:

```bash
turbo-run [script]
```

For example, if you want to run the `dev` command:

```bash
turbo-run dev
```

The tool automatically detects which packages have the `dev` command and provides an interactive interface for you to select which packages to run.

## Example

Assume you have the following packages in your project:

- `@vben/app`
- `@vben/admin`
- `@vben/website`

When you run:

```bash
turbo-run dev
```

Tools will:

1. Check which packages have the `dev` command
2. Display an interactive selection interface
3. Lets you select the package to run the command on
4. Use `pnpm --filter` to run commands in selected packages

## Notes

- Make sure your project uses pnpm as the package manager
- Make sure the target package defines the corresponding script command in `package.json`
- This tool needs to be run in the root directory of the monorepo project
