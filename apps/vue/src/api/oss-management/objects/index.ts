import { defHttp } from '/@/utils/http/axios';
import {
  OssObject,
  OssObjectCreate,
  OssObjectBulkDelete,
  OssObjectsResult,
  GetOssObjectRequest,
  GetOssObjectPagedRequest,
} from './model';
import { AxiosResponse } from 'axios';
import { isFunction } from '/@/utils/is';
import { UploadFileParams } from '/#/axios';
import { useAbpStoreWithOut } from '/@/store/modules/abp';
import { format } from '/@/utils/strings';
import { ContentTypeEnum } from '/@/enums/httpEnum';

export const uploadUrl = '/api/oss-management/objects/upload';
export const downloadUrl = '/api/files/static/{bucket}/p/{path}/{name}';

export function generateOssUrl(bucket: string, path: string, object: string) {
  if (path) {
    // Encode the URL of the Path part
    path = encodeURIComponent(path);
    if (path !== '.%2F' && path.endsWith('%2F')) {
      path = path.substring(0, path.length - 3);
    }
  }
  return format(downloadUrl, { bucket: bucket, path: path, name: object });
}

export const downloadBlob = (bucket: string, path: string, object: string) => {
  return defHttp.get<Blob>({
    url: generateOssUrl(bucket, path, object),
    headers: {
      accept: 'application/json',
    },
    responseType: 'blob',
  });
};

/**
 * Upload files in parts
 * @param params file parameters
 * @param event event parameters
 * @returns Response after successful upload
 */
export const uploadObject = (params: UploadFileParams, event: any) => {
  // Regular block size
  const chunkSize: number = params.chunkSize ?? 2 * 1024 * 1024;
  // Total number of blocks
  const totalChunks = Math.ceil(params.file.size / chunkSize);
  // Total size
  const totalSize = params.file.size;
  // file name
  const fileName = params.filename ?? (params.file as File).name;
  // Completed size
  let loadedSize = 0;
  // Return the wrapped result
  return new Promise<AxiosResponse<any>>(async (resolve, reject) => {
    function onPregress(progress: number, res: AxiosResponse) {
      // Callback upload progress
      if (isFunction(event)) {
        event({
          loaded: progress,
          total: totalSize,
        });
      }
      if (progress === totalSize) {
        if (!res.data) {
          let formatUrl = '/api/files/static/{bucket}/p/{path}/{name}';
          const abpStore = useAbpStoreWithOut();
          const { currentTenant } = abpStore.getApplication;
          if (currentTenant.id) {
            formatUrl = '/api/files/static/t/{tenantId}/{bucket}/p/{path}/{name}';
          }
          const path = encodeURIComponent(params.data?.path);
          res.data = {
            url: format(formatUrl, {
              bucket: params.data?.bucket,
              tenantId: currentTenant.id,
              path: path,
              name: fileName,
            }),
          };
        }
        resolve(res);
      }
    }
    function uploadFunc(num: number) {
      // Calculate shard index
      const nextSize = Math.min(num * chunkSize, params.file.size);
      // Cut file
      const fileData = params.file.slice((num - 1) * chunkSize, nextSize);
      // Current shard size
      const currentChunksSize = fileData.size;
      const requestConfig = {
        url: uploadUrl,
        // Set the timeout to a long time
        timeout: 600000,
      };
      const requestData = {
        file: fileData,
        chunkNumber: num,
        chunkSize: chunkSize,
        currentChunkSize: currentChunksSize,
        totalChunks: totalChunks,
        totalSize: totalSize,
        bucket: params.data?.bucket,
        path: params.data?.path,
        fileName: fileName,
      };
      return defHttp
        .uploadFile(requestConfig, {
          data: requestData,
          file: fileData,
        })
        .then((res) => {
          // current progress
          loadedSize += currentChunksSize;
          onPregress(loadedSize, res);
          return Promise.resolve(res);
        })
        .catch((error) => {
          return Promise.reject(error);
        });
    }
    // TODO: Support uploading multiple fragmented files at one time
    for (let num = 1; num <= totalChunks; num++) {
      try {
        // Upload the first fragment in a synchronous manner. If an error occurs, other fragments will not be uploaded.
        const res = await uploadFunc(num);
        onPregress(loadedSize, res);
      } catch (error) {
        return reject(error);
      }
    }
  });
};

export const createObject = (input: OssObjectCreate, file?: Blob) => {
  const formData = new window.FormData();
  formData.append('bucket', input.bucket);
  formData.append('path', input.path);
  formData.append('fileName', input.object);
  formData.append('overwrite', String(input.overwrite));
  input.expirationTime && formData.append('expirationTime', input.expirationTime.toString());
  file && formData.append('file', file);
  return defHttp.post<OssObject>({
    url: '/api/oss-management/objects',
    headers: {
      'Content-type': ContentTypeEnum.FORM_DATA,
    },
    data: formData,
  });
};

export const deleteObject = (input: GetOssObjectRequest) => {
  return defHttp.delete<void>({
    url: '/api/oss-management/objects',
    params: input,
  });
};

export const bulkDeleteObject = (input: OssObjectBulkDelete) => {
  return defHttp.post<void>({
    url: '/api/oss-management/objects/bulk-delete',
    data: input,
  });
};

export const getObject = (input: GetOssObjectRequest) => {
  return defHttp.get<OssObject>({
    url: '/api/oss-management/objects',
    params: input,
  });
};

export const getObjects = (input: GetOssObjectPagedRequest) => {
  return defHttp.get<OssObjectsResult>({
    url: '/api/oss-management/containes/objects',
    params: input,
  });
};
