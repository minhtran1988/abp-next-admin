export type Locale = 'en-US' | 'zh-CN';

export const messages: Record<Locale, Record<string, string>> = {
  'en-US': {
    cancel: 'Cancel',
    collapse: 'Collapse',
    confirm: 'Confirm',
    expand: 'Expand',
    prompt: 'Prompt',
    reset: 'Reset',
    submit: 'Submit',
  },
  'zh-CN': {
    cancel: 'cancel',
    collapse: 'collapse',
    confirm: 'confirm',
    expand: 'expand',
    prompt: 'prompt',
    reset: 'reset',
    submit: 'submit',
  },
};

export const getMessages = (locale: Locale) => messages[locale];
