// Default attributes of approval node
export const APPROVAL_PROPS = {
  assignedType: "ASSIGN_USER",
  mode: "AND",
  sign: false,
  nobody: {
    handler: "TO_PASS",
    assignedUser:[]
  },
  timeLimit:{
    timeout:{
      unit: "H",
      value: 0
    },
    handler:{
      type: "REFUSE",
      notify:{
        once: true,
        hour: 1
      }
    }
  },
  assignedUser:[],
  formPerms:[],
  selfSelect: {
    multiple: false
  },
  leaderTop: {
    endCondition: "TOP",
    endLevel: 1,
  },
  leader:{
    level: 1
  },
  role:[],
  refuse: {
    type: 'TO_END', //Reject rule TO_END TO_NODE TO_BEFORE
    target: '' //Reject to the node with the specified ID
  },
  formUser: ''
}

// Default attributes of root node
export const ROOT_PROPS = {
  assignedUser: [],
  formPerms:[]
}

// Conditional node default attributes
export const CONDITION_PROPS = {
  groupsType:"OR", //Conditional group logical relationship OR, AND
  groups:[
    {
      groupType:"AND", //Conditional relationship within the condition group OR, AND
      cids:[], //Conditional ID collection
      conditions:[] //Sub-conditions within the group
    }
  ],
  expression: "" //Customized expressions to flexibly construct logical relationships
}

// Default attributes of CC node
export const CC_PROPS = {
  shouldAdd: false,
  assignedUser: [],
  formPerms:[]
}

// Default properties of trigger node
export const TRIGGER_PROPS = {
  type: 'WEBHOOK',
  http:{
    method: 'GET', //Request method supports GET/POST
    url: '', //URL address, you can directly take parameters
    headers: [ //http header
      {
        name: '',
        isField: true,
        value: '' //Supports expressions ${xxx} xxx is the name of the form field
      }
    ],
    contentType: 'FORM', //Request parameter type
    params:[ //Request parameters
      {
        name: '',
        isField: true, //Is it a form field or custom?
        value: '' //Supports expressions ${xxx} xxx is the name of the form field
      }
    ],
    retry: 1,
    handlerByScript: false,
    success: 'function handlerOk(res) {\n  return true;\n}',
    fail: 'function handlerFail(res) {\n  return true;\n}'
  },
  email:{
    subject: '',
    to: [],
    content: ''
  }
}

// Default properties of delay node
export const DELAY_PROPS = {
  type: "FIXED", //Delay type FIXED: Delay for a fixed length of time after reaching the current node, AUTO: Delay to the time set by dateTime
  time: 0, //delay time
  unit: "M", //time unit D days H hours M minutes
  dateTime: "" //If the day does not exceed the set time point, it will be delayed to the specified time. When it arrives, it will be skipped without delay.
}

// HTTP EndPoint default properties
export const HTTPENDPOINT_PROPS = {
  path: '',
  methods: [],
  readCotntent: false,
  targetType: '',
  schema: '',
  authorize: false,
  policy: '',
}

export default {
  APPROVAL_PROPS,
  CC_PROPS,
  DELAY_PROPS,
  CONDITION_PROPS,
  ROOT_PROPS,
  TRIGGER_PROPS,
  HTTPENDPOINT_PROPS,
}

export const PrimaryNodes = [
  'ROOT',
  'APPROVAL',
  'CC',
  'DELAY',
  'TRIGGER',
  'HTTPENDPOINT'
];
