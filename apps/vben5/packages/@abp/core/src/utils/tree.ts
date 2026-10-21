interface TreeHelperConfig {
  children: string;
  id: string;
  pid: string;
}
const DEFAULT_CONFIG: TreeHelperConfig = {
  id: 'id',
  pid: 'pid',
  children: 'children',
};

const getConfig = (config: Partial<TreeHelperConfig>) =>
  Object.assign({}, DEFAULT_CONFIG, config);

// tree from list
export function listToTree<T = any>(
  list: any[],
  config: Partial<TreeHelperConfig> = {},
): T[] {
  const conf = getConfig(config) as TreeHelperConfig;
  const map: { [key: string]: any } = {};
  const roots: any[] = [];
  const { id, pid, children } = conf;

  // Put each element into the map to facilitate searching by id
  list.forEach((item) => {
    map[item[id]] = { ...item, [children]: [] };
  });

  // Build a tree structure
  list.forEach((item) => {
    const parentId = item[pid];
    if (parentId === null || parentId === undefined) {
      // root node
      roots.push(map[item[id]]);
    } else {
      // Non-root node, add it to the children array of the parent node
      if (map[parentId]) {
        map[parentId][children].push(map[item[id]]);
      }
    }
  });

  return roots;
}
