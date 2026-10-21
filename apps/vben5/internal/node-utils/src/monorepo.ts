import { dirname } from 'node:path';

import {
  getPackages as getPackagesFunc,
  getPackagesSync as getPackagesSyncFunc,
} from '@manypkg/get-packages';
import { findUpSync } from 'find-up';

/**
 * Find the root directory of Okura
 * @param cwd
 */
function findMonorepoRoot(cwd: string = process.cwd()) {
  const lockFile = findUpSync('pnpm-lock.yaml', {
    cwd,
    type: 'file',
  });
  return dirname(lockFile || '');
}

/**
 * Get all packages from Okura
 */
function getPackagesSync() {
  const root = findMonorepoRoot();
  return getPackagesSyncFunc(root);
}

/**
 * Get all packages from Okura
 */
async function getPackages() {
  const root = findMonorepoRoot();

  return await getPackagesFunc(root);
}

/**
 * Get the package specified by Okura
 */
async function getPackage(pkgName: string) {
  const { packages } = await getPackages();
  return packages.find((pkg) => pkg.packageJson.name === pkgName);
}

export { findMonorepoRoot, getPackage, getPackages, getPackagesSync };
