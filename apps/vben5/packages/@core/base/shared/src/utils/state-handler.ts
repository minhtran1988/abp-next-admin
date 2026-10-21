export class StateHandler {
  private condition: boolean = false;
  private rejectCondition: (() => void) | null = null;
  private resolveCondition: (() => void) | null = null;

  isConditionTrue(): boolean {
    return this.condition;
  }

  reset() {
    this.condition = false;
    this.clearPromises();
  }

  // When the trigger state is false, reject
  setConditionFalse() {
    this.condition = false;
    if (this.rejectCondition) {
      this.rejectCondition();
      this.clearPromises();
    }
  }

  // When the trigger status is true, resolve
  setConditionTrue() {
    this.condition = true;
    if (this.resolveCondition) {
      this.resolveCondition();
      this.clearPromises();
    }
  }

  // Return a Promise and wait for condition to become true
  waitForCondition(): Promise<void> {
    return new Promise((resolve, reject) => {
      if (this.condition) {
        resolve(); // If condition is already true, resolve immediately
      } else {
        this.resolveCondition = resolve;
        this.rejectCondition = reject;
      }
    });
  }

  // Clean up resolve/reject functions
  private clearPromises() {
    this.resolveCondition = null;
    this.rejectCondition = null;
  }
}
