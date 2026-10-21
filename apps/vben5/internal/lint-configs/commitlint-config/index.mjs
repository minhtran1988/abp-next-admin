import { execSync } from 'node:child_process';

import { getPackagesSync } from '@vben/node-utils';

const { packages } = getPackagesSync();

const allowedScopes = [
  ...packages.map((pkg) => pkg.packageJson.name),
  'project',
  'style',
  'lint',
  'ci',
  'dev',
  'deploy',
  'other',
];

// precomputed scope
const scopeComplete = execSync('git status --porcelain || true')
  .toString()
  .trim()
  .split('\n')
  .find((r) => ~r.indexOf('M  src'))
  ?.replaceAll(/(\/)/g, '%%')
  ?.match(/src%%((\w|-)*)/)?.[1]
  ?.replace(/s$/, '');

/**
 * @type {import('cz-git').UserConfig}
 */
const userConfig = {
  extends: ['@commitlint/config-conventional'],
  plugins: ['commitlint-plugin-function-rules'],
  prompt: {
    /** @use `pnpm commit :f` */
    alias: {
      b: 'build: bump dependencies',
      c: 'chore: update config',
      f: 'docs: fix typos',
      r: 'docs: update README',
      s: 'style: update code format',
    },
    allowCustomIssuePrefixs: false,
    // scopes: [...scopes, 'mock'],
    allowEmptyIssuePrefixs: false,
    customScopesAlign: scopeComplete ? 'bottom' : 'top',
    defaultScope: scopeComplete,
    // English
    typesAppend: [
      { name: 'workflow: workflow improvements', value: 'workflow' },
      { name: 'types:    type definition file changes', value: 'types' },
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
  rules: {
    /**
     * type[scope]: [function] description
     *
     * ^^^^^^^^^^^^^^ empty line.
     * - Something here
     */
    'body-leading-blank': [2, 'always'],
    /**
     * type[scope]: [function] description
     *
     * - something here
     *
     * ^^^^^^^^^^^^^^
     */
    'footer-leading-blank': [1, 'always'],
    /**
     * type[scope]: [function] description
     *      ^^^^^
     */
    'function-rules/scope-enum': [
      2, // level: error
      'always',
      (parsed) => {
        if (!parsed.scope || allowedScopes.includes(parsed.scope)) {
          return [true];
        }

        return [false, `scope must be one of ${allowedScopes.join(', ')}`];
      },
    ],
    /**
     * type[scope]: [function] description [No more than 108 characters]
     *      ^^^^^
     */
    'header-max-length': [2, 'always', 108],

    'scope-enum': [0],
    'subject-case': [0],
    'subject-empty': [2, 'never'],
    'type-empty': [2, 'never'],
    /**
     * type[scope]: [function] description
     * ^^^^
     */
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
        'types',
        'release',
      ],
    ],
  },
};

export default userConfig;
