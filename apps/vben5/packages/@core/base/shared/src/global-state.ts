/**
 * Globally reusable variables, components, and configurations are shared among various modules
 * Implemented through the singleton mode, the singleton must be careful not to be affected by the request, such as user information that needs to be obtained according to the request. If there is a need for SSR in the future, it will not be affected.
 */

interface ComponentsState {
  [key: string]: any;
}

interface MessageState {
  copyPreferencesSuccess?: (title: string, content?: string) => void;
}

export interface IGlobalSharedState {
  components: ComponentsState;
  message: MessageState;
}

class GlobalShareState {
  #components: ComponentsState = {};
  #message: MessageState = {};

  /**
   * Define message prompts for each scene within the framework
   */
  public defineMessage({ copyPreferencesSuccess }: MessageState) {
    this.#message = {
      copyPreferencesSuccess,
    };
  }

  public getComponents(): ComponentsState {
    return this.#components;
  }

  public getMessage(): MessageState {
    return this.#message;
  }

  public setComponents(value: ComponentsState) {
    this.#components = value;
  }
}

export const globalShareState = new GlobalShareState();
