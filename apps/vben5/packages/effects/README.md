## Effects Catalog

The `effects` directory is specifically used to store code and logic related to slight coupling. If your package has the following characteristics, it is recommended to place it in the `effects` directory:

- **State Management**: Use the state management framework `pinia`, and include parts for handling side effects (such as asynchronous operations, API calls).
- **User Preferences**: Use `@vben-core/preferences` to handle user preferences involving local storage or browser caching logic (such as using `localStorage`).
- **Navigation and Routing**: To handle scenarios such as navigation and page jumps, it is necessary to manage the logic of routing changes.
- **Component Library Dependencies**: Contains parts that are tightly coupled with a specific component library or rely on a large warehouse.

By classifying relevant code into the `effects` directory, the project structure can be made clearer and easier to maintain and expand.
