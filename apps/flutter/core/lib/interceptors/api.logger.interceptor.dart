import 'package:core/utils/logging.dart';
import 'package:dio/dio.dart';

class LoggerInterceptor extends Interceptor {
  @override
  void onRequest(RequestOptions options, RequestInterceptorHandler handler) {
    logger.debug('Network request starts: ${options.uri.toString()}');
    return handler.next(options);
  }

  @override
  void onResponse(Response response, ResponseInterceptorHandler handler) {
    logger.debug('Network request ended, status: ${response.statusCode}, message: ${response.statusMessage}');
    return handler.next(response);
  }

  @override
  void onError(DioException err, ErrorInterceptorHandler handler) {
    logger.error('Network request error', err, err.stackTrace);
    return handler.next(err);
  }
}
