interface TreeConfigOptions {
  // The name of the child attribute, default is 'children'
  childProps: string;
}

/**
 * @zh_CN Traverses the tree structure and returns the specified values in all nodes.
 * @param tree tree structure array
 * @param getValue function to get node value
 * @param options as the optional attribute name of the child node array.
 * @returns array of values specified in all nodes
 */
function traverseTreeValues<T, V>(
  tree: T[],
  getValue: (node: T) => V,
  options?: TreeConfigOptions,
): V[] {
  const result: V[] = [];
  const { childProps } = options || {
    childProps: 'children',
  };

  const dfs = (treeNode: T) => {
    const value = getValue(treeNode);
    result.push(value);
    const children = (treeNode as Record<string, any>)?.[childProps];
    if (!children) {
      return;
    }
    if (children.length > 0) {
      for (const child of children) {
        dfs(child);
      }
    }
  };

  for (const treeNode of tree) {
    dfs(treeNode);
  }
  return result.filter(Boolean);
}

/**
 * Filter the nodes of the given tree structure according to conditions and return an array of all matching nodes in the original order.
 * @param tree The root node array of the tree structure to be filtered.
 * @param filter Condition used to match each node.
 * @param options as the optional attribute name of the child node array.
 * @returns An array containing all matching nodes.
 */
function filterTree<T extends Record<string, any>>(
  tree: T[],
  filter: (node: T) => boolean,
  options?: TreeConfigOptions,
): T[] {
  const { childProps } = options || {
    childProps: 'children',
  };

  const _filterTree = (nodes: T[]): T[] => {
    return nodes.filter((node: Record<string, any>) => {
      if (filter(node as T)) {
        if (node[childProps]) {
          node[childProps] = _filterTree(node[childProps]);
        }
        return true;
      }
      return false;
    });
  };

  return _filterTree(tree);
}

/**
 * Conditionally remap sections of a given tree structure
 * @param tree The root node array of the tree structure to be filtered.
 * @param mapper Condition used to map each node.
 * @param options as the optional attribute name of the child node array.
 */
function mapTree<T, V extends Record<string, any>>(
  tree: T[],
  mapper: (node: T) => V,
  options?: TreeConfigOptions,
): V[] {
  const { childProps } = options || {
    childProps: 'children',
  };
  return tree.map((node) => {
    const mapperNode: Record<string, any> = mapper(node);
    if (mapperNode[childProps]) {
      mapperNode[childProps] = mapTree(mapperNode[childProps], mapper, options);
    }
    return mapperNode as V;
  });
}

/**
 * Recursively sort tree structure data
 * @param treeData - tree data array
 * @param sortFunction - sorting function, used to define sorting rules
 * @param options - configuration options, including child node attribute names
 * @returns sorted tree data
 */
function sortTree<T extends Record<string, any>>(
  treeData: T[],
  sortFunction: (a: T, b: T) => number,
  options?: TreeConfigOptions,
): T[] {
  const { childProps } = options || {
    childProps: 'children',
  };

  return treeData.toSorted(sortFunction).map((item) => {
    const children = item[childProps];
    if (children && Array.isArray(children) && children.length > 0) {
      return {
        ...item,
        [childProps]: sortTree(children, sortFunction, options),
      };
    }
    return item;
  });
}

export { filterTree, mapTree, sortTree, traverseTreeValues };
