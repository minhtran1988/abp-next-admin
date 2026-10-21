const fs = require('fs');
const path = require('path');
const { execSync } = require('child_process');

const scopes = fs
  .readdirSync(path.resolve(__dirname, 'src'), { withFileTypes: true })
  .filter((dirent) => dirent.isDirectory())
  .map((dirent) => dirent.name.replace(/s$/, ''));

// precomputed scope
const scopeComplete = execSync('git status --porcelain || true')
  .toString()
  .trim()
  .split('\n')
  .find((r) => ~r.indexOf('M  src'))
  ?.replace(/(\/)/g, '%%')
  ?.match(/src%%((\w|-)*)/)?.[1]
  ?.replace(/s$/, '');

/** @type {import('cz-git').UserConfig} */
module.exports = {
  ignores: [(commit) => commit.includes('init')],
  extends: ['@commitlint/config-conventional'],
  rules: {
    'body-leading-blank': [2, 'always'],
    'footer-leading-blank': [1, 'always'],
    'header-max-length': [2, 'always', 108],
    'subject-empty': [2, 'never'],
    'type-empty': [2, 'never'],
    'subject-case': [0],
    'type-enum': [
      2,
      'always',
      [
        'feat',
        'fix',
        'perf',
        'style',
        'docs',
        'test',
        'refactor',
        'build',
        'ci',
        'chore',
        'revert',
        'wip',
        'workflow',
        'types',
        'release',
      ],
    ],
  },
  prompt: {
    /** @use `yarn commit :f` */
    alias: {
      f: 'docs: fix typos',
      r: 'docs: update README',
      s: 'style: update code format',
      b: 'build: bump dependencies',
      c: 'chore: update config',
    },
    customScopesAlign: !scopeComplete ? 'top' : 'bottom',
    defaultScope: scopeComplete,
    scopes: [...scopes, 'mock'],
    allowEmptyIssuePrefixs: false,
    allowCustomIssuePrefixs: false,

    // English
    typesAppend: [
      { value: 'wip', name: 'wip:      work in process' },
      { value: 'workflow', name: 'workflow: workflow improvements' },
      { value: 'types', name: 'types:    type definition file changes' },
    ],

    // Chinese and English version
    // messages: {
    // type: 'Select the type you want to submit:',
    // scope: 'Select a submission scope (optional):',
    // customScope: 'Please enter a custom submission scope:',
    // subject: 'Fill in a short and concise description of the change:\n',
    // body: 'Fill in a more detailed description of the change (optional). Use "|" to wrap:\n',
    // breaking: 'List incompatible breaking changes (optional). Use "|" to wrap:\n',
    // footerPrefixsSelect: 'Select the associated issue prefix (optional):',
    // customFooterPrefixs: 'Enter custom issue prefix:',
    // footer: 'List related issues (optional) For example: #31, #I3244:\n',
    // confirmCommit: 'Do you want to submit or modify the commit?',
    // },
    // types: [
    // { value: 'feat', name: 'feat: new features' },
    // { value: 'fix', name: 'fix: fix defects' },
    // { value: 'docs', name: 'docs: Documentation Changes' },
    // { value: 'style', name: 'style: code format' },
    // { value: 'refactor', name: 'refactor: code refactoring' },
    // { value: 'perf', name: 'perf: performance optimization' },
    // { value: 'test', name: 'test: Add missing tests or existing test changes' },
    // { value: 'build', name: 'build: build process, external dependency changes (such as upgrading npm packages, modifying packaging configuration, etc.)' },
    // { value: 'ci', name: 'ci: Modify CI configuration, script' },
    // { value: 'revert', name: 'revert: rollback commit' },
    // { value: 'chore', name: 'chore: changes to the build process or auxiliary tools and libraries (does not affect source files, test cases)' },
    // { value: 'wip', name: 'wip: under development' },
    // { value: 'workflow', name: 'workflow: Workflow improvement' },
    // { value: 'types', name: 'types: type definition file modification' },
    // ],
    // emptyScopesAlias: 'empty: don't fill in',
    // customScopesAlias: 'custom: custom',
  },
};
