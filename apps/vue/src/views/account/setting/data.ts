import { FormSchema } from '/@/components/Form/index';
import { useSettings } from '/@/hooks/abp/useSettings';
import { useLocalization } from '/@/hooks/abp/useLocalization';

export interface ListItem {
  key: string;
  title: string;
  description: string;
  extra?: string;
  avatar?: string;
  color?: string;
}

// list of tabs
export function getSettingList() {
  const { L } = useLocalization('AbpAccount');
  return [
    {
      key: '1',
      name: L('BasicSettings'),
      component: 'BaseSetting',
    },
    {
      key: '2',
      name: L('SecuritySettings'),
      component: 'SecureSetting',
    },
    {
      key: '3',
      name: L('Binding'),
      component: 'AccountBind',
    },
    {
      key: '4',
      name: L('Notifies'),
      component: 'MsgNotify',
    },
    {
      key: '5',
      name: L('Authenticator'),
      component: 'Authenticator',
    },
  ];
}

// Basic settings form
export function getBaseSetschemas(): FormSchema[] {
  const { settingProvider } = useSettings();
  const { L } = useLocalization('AbpAccount');
  return [
    {
      field: 'userName',
      component: 'Input',
      label: L('DisplayName:UserName'),
      dynamicDisabled: () => {
        return !settingProvider.isTrue('Abp.Identity.User.IsUserNameUpdateEnabled');
      },
      colProps: { span: 18 },
    },
    {
      field: 'email',
      component: 'Input',
      label: L('DisplayName:Email'),
      dynamicDisabled: () => {
        return !settingProvider.isTrue('Abp.Identity.User.IsEmailUpdateEnabled');
      },
      colProps: { span: 18 },
    },
    {
      field: 'surname',
      component: 'Input',
      label: L('DisplayName:Surname'),
      colProps: { span: 18 },
    },
    {
      field: 'name',
      component: 'Input',
      label: L('DisplayName:Name'),
      colProps: { span: 18 },
    },
    {
      field: 'phoneNumber',
      component: 'Input',
      label: L('DisplayName:PhoneNumber'),
      colProps: { span: 18 },
    },
    {
      field: 'extraProperties.AvatarUrl',
      component: 'Input',
      label: 'AvatarUrl',
      ifShow: false,
    },
    {
      field: 'concurrencyStamp',
      component: 'Input',
      label: 'ConcurrencyStamp',
      ifShow: false,
    },
  ];
}

// security settings list
export function getSecureSettingList(): ListItem[] {
  const { L } = useLocalization('AbpAccount');
  return [
    {
      key: '1',
      title: L('DisplayName:Password'),
      description: L('ResetMyPassword'),
      extra: L('Edit'),
    },
    {
      key: '2',
      title: L('DisplayName:PhoneNumber'),
      description: 'Binded mobile phone::138****8293',
      extra: L('Edit'),
    },
  ];
}
export const secureSettingList: ListItem[] = [
  {
    key: '1',
    title: 'Account Password',
    description: 'Current password strength::strong',
    extra: 'modify',
  },
  {
    key: '2',
    title: 'Secret mobile phone',
    description: 'Binded mobile phone::138****8293',
    extra: 'modify',
  },
  {
    key: '3',
    title: 'Security issue',
    description: 'No security questions are set, security questions can effectively protect account security',
    extra: 'modify',
  },
  {
    key: '4',
    title: 'Alternate Email',
    description: 'Binded email address: ant***sign.com',
    extra: 'modify',
  },
  {
    key: '5',
    title: 'MFA Device',
    description: 'The MFA device is not bound. After binding, you can confirm again',
    extra: 'modify',
  },
];

// Account binding list
export const accountBindList: ListItem[] = [
  {
    key: '1',
    title: 'Bind Taobao',
    description: 'Currently no Taobao account is bound',
    extra: 'binding',
    avatar: 'ri:taobao-fill',
    color: '#ff4000',
  },
  {
    key: '2',
    title: 'Bind Alipay',
    description: 'Alipay account is not currently bound',
    extra: 'binding',
    avatar: 'fa-brands:alipay',
    color: '#2eabff',
  },
  {
    key: '3',
    title: 'Binding DingTalk',
    description: 'Currently no DingTalk account is bound',
    extra: 'binding',
    avatar: 'ri:dingding-fill',
    color: '#2eabff',
  },
];

// New message notification list
export const msgNotifyList: ListItem[] = [
  {
    key: '1',
    title: 'Account Password',
    description: 'Messages from other users will be notified in the form of site messages',
  },
  {
    key: '2',
    title: 'System Message',
    description: 'System messages will be notified in the form of site messages',
  },
  {
    key: '3',
    title: 'To-do tasks',
    description: 'To-do tasks will be notified in the form of in-site messages',
  },
];
