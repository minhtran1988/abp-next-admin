import { NotificationContentType } from "/@/api/messages/notifications/model";

export interface ListItem {
  id: string;
  avatar: string;
  // The title content of the notification
  title: string;
  // Whether to display strikethrough on the title
  titleDelete?: boolean;
  datetime: string;
  type: string;
  read?: boolean;
  description: string;
  clickClose?: boolean;
  extra?: string;
  color?: string;
  contentType?: NotificationContentType;
}

export interface TabItem {
  key: string;
  name: string;
  list: ListItem[];
  unreadlist?: ListItem[];
}

export const tabListData: TabItem[] = [
  {
    key: '1',
    name: 'notification',
    list: [
      {
        id: '000000001',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/ThXAXghbEsBCCSDihZxY.png',
        title: 'You have received 14 new weekly newspapers',
        description: '',
        datetime: '2017-08-09',
        type: '1',
      },
      {
        id: '000000002',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/OKJXDXrmkNshAMvwtvhu.png',
        title: 'Qu Nini whom you recommended has passed the third round of interviews',
        description: '',
        datetime: '2017-08-08',
        type: '1',
      },
      {
        id: '000000003',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/kISTdvpyTAhtGxpovNWd.png',
        title: 'This template can distinguish between multiple notification types',
        description: '',
        datetime: '2017-08-07',
        // read: true,
        type: '1',
      },
      {
        id: '000000004',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/GvqBnKhFgObvnSGkDsje.png',
        title: 'The icons on the left are used to distinguish different types',
        description: '',
        datetime: '2017-08-07',
        type: '1',
      },
      {
        id: '000000005',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/GvqBnKhFgObvnSGkDsje.png',
        title:
          'The title can be set to automatically display ellipsis. In this example, the number of title lines has been set to 1 line. If the content exceeds 1 line, it will be automatically truncated and the tooltip will be supported to display the complete title. ',
        description: '',
        datetime: '2017-08-07',
        type: '1',
      },
      {
        id: '000000006',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/GvqBnKhFgObvnSGkDsje.png',
        title: 'The icons on the left are used to distinguish different types',
        description: '',
        datetime: '2017-08-07',
        type: '1',
      },
      {
        id: '000000007',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/GvqBnKhFgObvnSGkDsje.png',
        title: 'The icons on the left are used to distinguish different types',
        description: '',
        datetime: '2017-08-07',
        type: '1',
      },
      {
        id: '000000008',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/GvqBnKhFgObvnSGkDsje.png',
        title: 'The icons on the left are used to distinguish different types',
        description: '',
        datetime: '2017-08-07',
        type: '1',
      },
      {
        id: '000000009',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/GvqBnKhFgObvnSGkDsje.png',
        title: 'The icons on the left are used to distinguish different types',
        description: '',
        datetime: '2017-08-07',
        type: '1',
      },
      {
        id: '000000010',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/GvqBnKhFgObvnSGkDsje.png',
        title: 'The icons on the left are used to distinguish different types',
        description: '',
        datetime: '2017-08-07',
        type: '1',
      },
    ],
  },
  {
    key: '2',
    name: 'Message',
    list: [
      {
        id: '000000006',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/fcHMVNCjPOsbUGdEduuv.jpeg',
        title: 'Qu Lili commented on you',
        description: 'Description information description information description information',
        datetime: '2017-08-07',
        type: '2',
        clickClose: true,
      },
      {
        id: '000000007',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/fcHMVNCjPOsbUGdEduuv.jpeg',
        title: 'Zhu Qianyou replied to you',
        description: 'This template is used to remind you who has interacted with you',
        datetime: '2017-08-07',
        type: '2',
        clickClose: true,
      },
      {
        id: '000000008',
        avatar: 'https://gw.alipayobjects.com/zos/rmsportal/fcHMVNCjPOsbUGdEduuv.jpeg',
        title: 'title',
        description:
          'Please move the mouse here to test how long messages will be handled here. The maximum number of description lines set in this example is 2. Description content exceeding 2 lines will be omitted and the complete content can be viewed through tooltip',
        datetime: '2017-08-07',
        type: '2',
        clickClose: true,
      },
    ],
  },
  {
    key: '3',
    name: 'To-do',
    list: [
      {
        id: '000000009',
        avatar: '',
        title: 'task name',
        description: 'The task needs to be started before 2017-01-12 20:00',
        datetime: '',
        extra: 'not started',
        color: '',
        type: '3',
      },
      {
        id: '000000010',
        avatar: '',
        title: 'Third-party emergency code changes',
        description: 'Guan Lin needs to complete the code change task before 2017-01-07',
        datetime: '',
        extra: 'Expires immediately',
        color: 'red',
        type: '3',
      },
      {
        id: '000000011',
        avatar: '',
        title: 'Information Security Examination',
        description: 'Assign Zhuer to complete the update and release before 2017-01-09',
        datetime: '',
        extra: 'It took 8 days',
        color: 'gold',
        type: '3',
      },
      {
        id: '000000012',
        avatar: '',
        title: 'ABCD version release',
        description: 'Assign Zhuer to complete the update and release before 2017-01-09',
        datetime: '',
        extra: 'in progress',
        color: 'blue',
        type: '3',
      },
    ],
  },
];
