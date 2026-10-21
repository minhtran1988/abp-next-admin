import { promises as fs } from 'node:fs';
import { join, normalize } from 'node:path';

const rootDir = process.cwd();

// Control the number of concurrency to avoid creating too many concurrent tasks
const CONCURRENCY_LIMIT = 10;

// Directories that need to be skipped to avoid entering these directories for cleaning
const SKIP_DIRS = new Set(['.DS_Store', '.git', '.idea', '.vscode']);

/**
 * Process individual files/directory entries
 * @param {string} currentDir - current directory path
 * @param {string} item - file/directory name
 * @param {string[]} targets - list of targets to delete
 * @param {number} _depth - current recursion depth
 * @returns {Promise<boolean>} - whether further recursive processing is required
 */
async function processItem(currentDir, item, targets, _depth) {
  // skip special directories
  if (SKIP_DIRS.has(item)) {
    return false;
  }

  try {
    const itemPath = normalize(join(currentDir, item));

    if (targets.includes(item)) {
      // Delete directly when matching the target directory or file
      await fs.rm(itemPath, { force: true, recursive: true });
      console.log(`✅ Deleted: ${itemPath}`);
      return false; // Deleted, no recursion required
    }

    // Use the withFileTypes option of readdir to avoid additional lstat calls
    return true; // Recursion may be required, decided by the caller
  } catch (error) {
    // More detailed error information
    if (error.code === 'ENOENT') {
      // The file does not exist and may have been deleted. This is normal.
      return false;
    } else if (error.code === 'EPERM' || error.code === 'EACCES') {
      console.error(`❌ Permission denied: ${item} in ${currentDir}`);
    } else {
      console.error(
        `❌ Error handling item ${item} in ${currentDir}: ${error.message}`,
      );
    }
    return false;
  }
}

/**
 * Recursively search and delete the target directory (concurrency optimized version)
 * @param {string} currentDir - the currently traversed directory path
 * @param {string[]} targets - list of targets to delete
 * @param {number} depth - the current recursion depth, to avoid excessively deep recursion
 */
async function cleanTargetsRecursively(currentDir, targets, depth = 0) {
  // Limit the recursion depth to avoid infinite recursion
  if (depth > 10) {
    console.warn(`Max recursion depth reached at: ${currentDir}`);
    return;
  }

  let dirents;
  try {
    // Use the withFileTypes option to obtain file type information at once to avoid subsequent lstat calls
    dirents = await fs.readdir(currentDir, { withFileTypes: true });
  } catch (error) {
    // If the directory cannot be read, it may have been deleted or has insufficient permissions
    console.warn(`Cannot read directory ${currentDir}: ${error.message}`);
    return;
  }

  // Process in batches to control the number of concurrencies
  for (let i = 0; i < dirents.length; i += CONCURRENCY_LIMIT) {
    const batch = dirents.slice(i, i + CONCURRENCY_LIMIT);

    const tasks = batch.map(async (dirent) => {
      const item = dirent.name;
      const shouldRecurse = await processItem(currentDir, item, targets, depth);

      // If it is a directory and has not been deleted, process it recursively
      if (shouldRecurse && dirent.isDirectory()) {
        const itemPath = normalize(join(currentDir, item));
        return cleanTargetsRecursively(itemPath, targets, depth + 1);
      }

      return null;
    });

    // Concurrently execute the tasks of the current batch
    const results = await Promise.allSettled(tasks);

    // Check if there are any failed tasks (optional: for debugging)
    const failedTasks = results.filter(
      (result) => result.status === 'rejected',
    );
    if (failedTasks.length > 0) {
      console.warn(
        `${failedTasks.length} tasks failed in batch starting at index ${i} in directory: ${currentDir}`,
      );
    }
  }
}

(async function startCleanup() {
  // Directory and file name to be deleted
  const targets = ['node_modules', 'dist', '.turbo', 'dist.zip'];
  const deleteLockFile = process.argv.includes('--del-lock');
  const cleanupTargets = [...targets];

  if (deleteLockFile) {
    cleanupTargets.push('pnpm-lock.yaml');
  }

  console.log(
    `🚀 Starting cleanup of targets: ${cleanupTargets.join(', ')} from root: ${rootDir}`,
  );

  const startTime = Date.now();

  try {
    // First count the number of targets to be deleted
    console.log('📊 Scanning for cleanup targets...');

    await cleanTargetsRecursively(rootDir, cleanupTargets);

    const endTime = Date.now();
    const duration = (endTime - startTime) / 1000;

    console.log(
      `✨ Cleanup process completed successfully in ${duration.toFixed(2)}s`,
    );
  } catch (error) {
    console.error(`💥 Unexpected error during cleanup: ${error.message}`);
    process.exit(1);
  }
})();
