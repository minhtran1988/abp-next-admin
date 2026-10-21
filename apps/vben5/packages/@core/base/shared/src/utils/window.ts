interface OpenWindowOptions {
  noopener?: boolean;
  noreferrer?: boolean;
  target?: '_blank' | '_parent' | '_self' | '_top' | string;
}

/**
 * URL opens in new window.
 *
 * @param url - the URL that needs to be opened.
 * @param options - Options for opening the window.
 */
function openWindow(url: string, options: OpenWindowOptions = {}): void {
  // Destructure and set default values
  const { noopener = true, noreferrer = true, target = '_blank' } = options;

  // Create attribute string based on options
  const features = [noopener && 'noopener=yes', noreferrer && 'noreferrer=yes']
    .filter(Boolean)
    .join(',');

  // open window
  window.open(url, target, features);
}

/**
 * Open route in new window.
 * @param path
 */
function openRouteInNewWindow(path: string) {
  const { hash, origin } = location;
  const fullPath = path.startsWith('/') ? path : `/${path}`;
  const url = `${origin}${hash && !fullPath.startsWith('/#') ? '/#' : ''}${fullPath}`;
  openWindow(url, { target: '_blank' });
}

export { openRouteInNewWindow, openWindow };
