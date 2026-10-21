// The axios configuration can be changed according to the project. You only need to change this file, and other files can be left unchanged.
// The axios configuration can be changed according to the project, just change the file, other files can be left unchanged

import type { AxiosResponse } from 'axios';
import type { RequestOptions, Result } from '/#/axios';
import type { AxiosTransform, CreateAxiosOptions } from './axiosTransform';
import { VAxios } from './Axios';
import { checkResponse } from './checkStatus';
import { useGlobSetting } from '/@/hooks/setting';
import { useMessage } from '/@/hooks/web/useMessage';
import { RequestEnum, ResultEnum, ContentTypeEnum } from '/@/enums/httpEnum';
import { isString } from '/@/utils/is';
import { getToken } from '/@/utils/auth';
import { useI18n } from '/@/hooks/web/useI18n';
import { setObjToUrlParams, deepMerge } from '/@/utils';
import { useErrorLogStoreWithOut } from '/@/store/modules/errorLog';
import { joinTimestamp, formatRequestDate } from './helper';
import { useLocaleStoreWithOut } from '/@/store/modules/locale';
import axios from 'axios';

const globSetting = useGlobSetting();
const urlPrefix = globSetting.urlPrefix;
const { createMessage, createErrorModal } = useMessage();

/**
 * @description: Data processing, easy to distinguish between multiple processing methods
 */
const transform: AxiosTransform = {
  /**
   * @description: Process request data. If the data is not in the expected format, an error can be thrown directly
   */
  transformRequestHook: (res: AxiosResponse<Result>, options: RequestOptions) => {
    const { t } = useI18n();
    const { isReturnNativeResponse } = options;
    // Whether to return the native response header. For example: use this attribute when you need to obtain the response header.
    if (isReturnNativeResponse) {
      return res;
    }

    const { data } = res;

    // Process the packaging results
    if (res.headers['_abpwrapresult'] === 'true') {
      if (!data) {
        // return '[HTTP] Request has no return value';
        throw new Error(t('sys.api.apiRequestFailed'));
      }

      const { code, result, message, details } = data;
      const hasSuccess = data && Reflect.has(data, 'code') && code === ResultEnum.CODE;
      if (hasSuccess) {
        return result;
      }

      const title = details ? message : t('sys.api.errorTip');
      const content = details ? details : message;
      if (options.errorMessageMode === 'modal') {
        createErrorModal({ title: title, content: content });
      } else if (options.errorMessageMode === 'message') {
        createMessage.error(content);
      }

      throw new Error(content || t('sys.api.apiRequestFailed'));
    }

    return data;
  },

  // Process config before requesting
  beforeRequestHook: (config, options) => {
    const { apiUrl, joinPrefix, joinParamsToUrl, formatDate, joinTime = true, urlPrefix } = options;

    if (joinPrefix) {
      config.url = `${urlPrefix}${config.url}`;
    }

    if (apiUrl && isString(apiUrl)) {
      config.url = `${apiUrl}${config.url}`;
    }
    const params = config.params || {};
    const data = config.data || false;
    formatDate && data && !isString(data) && formatRequestDate(data);
    if (config.method?.toUpperCase() === RequestEnum.GET) {
      if (!isString(params)) {
        // Add a timestamp parameter to the get request to avoid getting data from the cache.
        config.params = Object.assign(params || {}, joinTimestamp(joinTime, false));
      } else {
        // Compatible with restful style
        config.url = config.url + params + `${joinTimestamp(joinTime, true)}`;
        config.params = undefined;
      }
    } else {
      if (!isString(params)) {
        formatDate && formatRequestDate(params);
        if (
          (Reflect.has(config, 'data') && config.data && Object.keys(config.data).length > 0) ||
          config.data instanceof FormData
        ) {
          config.data = data;
          config.params = params;
        } else if (
          Reflect.has(config, 'headers') &&
          config.headers &&
          config.headers['Content-Type'] !== ContentTypeEnum.FORM_DATA
        ) {
          // Prevent form-data type data from being cleared
          // For non-GET requests, if no data is provided, params will be treated as data.
          config.data = params;
          config.params = undefined;
        }
        if (joinParamsToUrl) {
          config.url = setObjToUrlParams(
            config.url as string,
            Object.assign({}, config.params, config.data),
          );
        }
      } else {
        // Compatible with restful style
        config.url = config.url + params;
        config.params = undefined;
      }
    }
    return config;
  },

  /**
   * @description: Request interceptor processing
   */
  requestInterceptors: (config, options) => {
    config.headers = config.headers ?? {};
    // Process config before requesting
    const token = getToken();
    if (token && (config as Recordable)?.requestOptions?.withToken !== false) {
      // jwt token
      (config as Recordable).headers.Authorization = options.authenticationScheme
        ? `${options.authenticationScheme} ${token}`
        : token;
    }
    if ((config as Recordable)?.requestOptions?.withAcceptLanguage !== false) {
      const localeStore = useLocaleStoreWithOut();
      config.headers['Accept-Language'] = localeStore.getLocale;
      if (config.headers['Accept-Language'] == 'zh_CN') {
        config.headers['Accept-Language'] = 'zh-Hans';
      }
    }
    return config;
  },

  /**
   * @description: response interceptor processing
   */
  responseInterceptors: (res: AxiosResponse<any>) => {
    return res;
  },

  /**
   * @description: Response error handling
   */
  responseInterceptorsCatch: (_, error: any) => {
    // const { t } = useI18n();
    const errorLogStore = useErrorLogStoreWithOut();
    errorLogStore.addAjaxErrorInfo(error);

    if (axios.isCancel(error)) {
      return Promise.reject(error);
    }

    if (error.code && ['ECONNABORTED', 'ETIMEDOUT'].includes(error.code)) {
      const { t } = useI18n();
      const timeout = t('sys.api.apiTimeoutMessage');
      createMessage.error(timeout);
      return Promise.reject(timeout);
    }

    const resMessage = checkResponse(error.response);
    return Promise.reject(resMessage ?? error);
  },
};

function createAxios(opt?: Partial<CreateAxiosOptions>) {
  return new VAxios(
    deepMerge(
      {
        // See https://developer.mozilla.org/en-US/docs/Web/HTTP/Authentication#authentication_schemes
        // authentication schemes，e.g: Bearer
        // authenticationScheme: 'Bearer',
        authenticationScheme: 'Bearer',
        timeout: 30 * 1000,
        // Basic interface address
        // baseURL: globSetting.apiUrl,

        headers: {
          'Content-Type': ContentTypeEnum.JSON,
          // Cancel backend cookie redirection
          'X-Request-From': 'vben',
        },
        // If it is form-data format
        // headers: { 'Content-Type': ContentTypeEnum.FORM_URLENCODED },
        // Data processing method
        transform,
        xsrfCookieName: 'XSRF-TOKEN',
        // ASP.NET Core
        xsrfHeaderName: 'RequestVerificationToken',
        // Configuration items, the following options can be overridden in independent interface requests
        requestOptions: {
          // Add prefix to url by default
          joinPrefix: true,
          // Whether to return the native response header. For example: use this attribute when you need to obtain the response header.
          isReturnNativeResponse: false,
          // Need to process the returned data
          isTransformRequestResult: false,
          isTransformResponse: true,
          // Add parameters to url when making post request
          joinParamsToUrl: false,
          // Format submission parameter time
          formatDate: true,
          // Message prompt type
          errorMessageMode: 'message',
          // Interface address
          apiUrl: globSetting.apiUrl,
          // Interface splicing address
          urlPrefix: urlPrefix,
          // Whether to add timestamp
          joinTime: true,
          // Ignore duplicate requests
          ignoreCancelToken: true,
          // Whether to carry token
          withToken: true,
          // Whether to carry the Accept-Language header
          withAcceptLanguage: true,
          retryRequest: {
            isOpenRetry: true,
            count: 5,
            waitTime: 100,
          },
        },
      },
      opt || {},
    ),
  );
}
export const defHttp = createAxios();

// other api url
// export const otherHttp = createAxios({
//   requestOptions: {
//     apiUrl: 'xxx',
//     urlPrefix: 'xxx',
//   },
// });
