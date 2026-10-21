interface GroupItem {
  title: string;
  icon: string;
  color: string;
  desc: string;
  date: string;
  group: string;
}

interface NavItem {
  title: string;
  icon: string;
  color: string;
}

interface DynamicInfoItem {
  avatar: string;
  name: string;
  date: string;
  desc: string;
}

export const navItems: NavItem[] = [
  {
    title: 'Home',
    icon: 'ion:home-outline',
    color: '#1fdaca',
  },
  {
    title: 'Dashboard',
    icon: 'ion:grid-outline',
    color: '#bf0c2c',
  },
  {
    title: 'Component',
    icon: 'ion:layers-outline',
    color: '#e18525',
  },
  {
    title: 'System Management',
    icon: 'ion:settings-outline',
    color: '#3fb27f',
  },
  {
    title: 'Permission Management',
    icon: 'ion:key-outline',
    color: '#4daf1bc9',
  },
  {
    title: 'Chart',
    icon: 'ion:bar-chart-outline',
    color: '#00d8ff',
  },
];

export const dynamicInfoItems: DynamicInfoItem[] = [
  {
    avatar: 'dynamic-avatar-1|svg',
    name: 'William',
    date: 'just',
    desc: `Created project <a>Vue</a> in <a>Open Source Group</a>`,
  },
  {
    avatar: 'dynamic-avatar-2|svg',
    name: 'Aiwen',
    date: '1 hour ago',
    desc: `Following <a>William</a> `,
  },
  {
    avatar: 'dynamic-avatar-3|svg',
    name: 'Chris',
    date: '1 day ago',
    desc: `Published <a>Personal News</a> `,
  },
  {
    avatar: 'dynamic-avatar-4|svg',
    name: 'Vben',
    date: '2 days ago',
    desc: `Publish article <a>How to write a Vite plug-in</a> `,
  },
  {
    avatar: 'dynamic-avatar-5|svg',
    name: 'Pete',
    date: '3 days ago',
    desc: `Replied to <a>Jack</a>'s question <a>How to optimize the project? </a>`,
  },
  {
    avatar: 'dynamic-avatar-6|svg',
    name: 'Jack',
    date: '1 week ago',
    desc: `Close the question <a>How to run the project</a> `,
  },
  {
    avatar: 'dynamic-avatar-1|svg',
    name: 'William',
    date: '1 week ago',
    desc: `Published <a>Personal News</a> `,
  },
  {
    avatar: 'dynamic-avatar-1|svg',
    name: 'William',
    date: '2021-04-01 20:00',
    desc: `Pushed the code to <a>Github</a>`,
  },
];

export const groupItems: GroupItem[] = [
  {
    title: 'Github',
    icon: 'carbon:logo-github',
    color: '',
    desc: 'Do not wait for opportunities; create them.',
    group: 'Open source group',
    date: '2021-04-01',
  },
  {
    title: 'Vue',
    icon: 'ion:logo-vue',
    color: '#3fb27f',
    desc: 'What you are now determines who you will be in the future. ',
    group: 'algorithm group',
    date: '2021-04-01',
  },
  {
    title: 'Html5',
    icon: 'ion:logo-html5',
    color: '#e18525',
    desc: 'No talent is more important than hard work. ',
    group: 'fishing at work',
    date: '2021-04-01',
  },
  {
    title: 'Angular',
    icon: 'ion:logo-angular',
    color: '#bf0c2c',
    desc: 'Passion and desire can overcome all obstacles. ',
    group: 'UI',
    date: '2021-04-01',
  },
  {
    title: 'React',
    icon: 'bx:bxl-react',
    color: '#00d8ff',
    desc: 'A healthy body is the cornerstone of achieving goals. ',
    group: 'Technical Cow',
    date: '2021-04-01',
  },
  {
    title: 'Js',
    icon: 'ion:logo-javascript',
    color: '#4daf1bc9',
    desc: 'The road is walked, not imagined. ',
    group: 'architecture group',
    date: '2021-04-01',
  },
];
