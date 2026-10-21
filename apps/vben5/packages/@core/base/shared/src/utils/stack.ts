/**
 * @zh_CN stack data structure
 */
export class Stack<T> {
  /**
   * @zh_CN Number of elements in the stack
   */
  get size() {
    return this.items.length;
  }
  /**
   * @zh_CN Whether to remove duplicates
   */
  private readonly dedup: boolean;
  /**
   * @zh_CN stack elements
   */
  private items: T[] = [];

  /**
   * @zh_CN The maximum capacity of the stack
   */
  private readonly maxSize?: number;

  constructor(dedup = true, maxSize?: number) {
    this.maxSize = maxSize;
    this.dedup = dedup;
  }

  /**
   * @zh_CN Clear the elements in the stack
   */
  clear() {
    this.items.length = 0;
  }

  /**
   * @zh_CN View the top element of the stack
   * @returns top element of stack
   */
  peek(): T | undefined {
    return this.items[this.items.length - 1];
  }

  /**
   * @zh_CN pop out of stack
   * @returns top element of stack
   */
  pop(): T | undefined {
    return this.items.pop();
  }

  /**
   * @zh_CN push into stack
   * @param items Elements to be pushed onto the stack
   */
  push(...items: T[]) {
    items.forEach((item) => {
      // Remove duplicates
      if (this.dedup) {
        const index = this.items.indexOf(item);
        if (index !== -1) {
          this.items.splice(index, 1);
        }
      }
      this.items.push(item);
      if (this.maxSize && this.items.length > this.maxSize) {
        this.items.splice(0, this.items.length - this.maxSize);
      }
    });
  }
  /**
   * @zh_CN Remove elements from the stack
   * @param itemList The list of elements to be removed
   */
  remove(...itemList: T[]) {
    this.items = this.items.filter((i) => !itemList.includes(i));
  }
  /**
   * @zh_CN retain elements in the stack
   * @param itemList The list of elements to retain
   */
  retain(itemList: T[]) {
    this.items = this.items.filter((i) => itemList.includes(i));
  }

  /**
   * @zh_CN Convert to array
   * @returns array of elements in the stack
   */
  toArray(): T[] {
    return [...this.items];
  }
}

/**
 * @zh_CN Create a stack instance
 * @param dedup whether to remove duplicates
 * @param maxSize the maximum capacity of the stack
 * @returns stack instance
 */
export const createStack = <T>(dedup = true, maxSize?: number) =>
  new Stack<T>(dedup, maxSize);
