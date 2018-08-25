#include "IUnityInterface.h"
#include "debug.h"
#include <memory>
#include <opus.h>

namespace {
  std::unique_ptr<debug> g_debug;
}

extern "C" {
  void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API SetDebugLogFunc(debug::log_func func)
  {
    g_debug = std::make_unique<debug>(func);
  }

  UNITY_INTERFACE_EXPORT OpusEncoder* UNITY_INTERFACE_API OpusEncoderCreate(
    opus_int32 sampling_frequency,
    int channels,
    int application,
    int *error
  )
  {
    return opus_encoder_create(sampling_frequency, channels, application, error);
  }

  UNITY_INTERFACE_EXPORT opus_int32 UNITY_INTERFACE_API OpusEncode(
    OpusEncoder* encoder,
    const opus_int16* pcm,
    int frame_size,
    unsigned char* data,
    opus_int32 max_data_bytes
  )
  {
    return opus_encode(encoder, pcm, frame_size, data, max_data_bytes);
  }

  UNITY_INTERFACE_EXPORT opus_int32 UNITY_INTERFACE_API OpusEncodeFloat(
    OpusEncoder* encoder,
    const float* pcm,
    int frame_size,
    unsigned char* data,
    opus_int32 max_data_bytes
  )
  {
    return opus_encode_float(encoder, pcm, frame_size, data, max_data_bytes);
  }

  UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API OpusEncoderDestroy(
    OpusEncoder* encoder
  )
  {
    opus_encoder_destroy(encoder);
  }

  UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API OpusEncoderSetBitrate(
    OpusEncoder* encoder,
    int bitrate
  )
  {
    return opus_encoder_ctl(encoder, OPUS_SET_BITRATE(bitrate));
  }

  UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API OpusEncoderSetComplexity(
    OpusEncoder* encoder,
    int complexity
  )
  {
    return opus_encoder_ctl(encoder, OPUS_SET_COMPLEXITY(complexity));
  }

  UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API OpusEncoderSetSignal(
    OpusEncoder* encoder,
    int signal
  )
  {
    return opus_encoder_ctl(encoder, OPUS_SET_SIGNAL(signal));
  }

  UNITY_INTERFACE_EXPORT OpusDecoder* UNITY_INTERFACE_API OpusDecoderCreate(
    opus_int32 sampling_frequency,
    int channels,
    int *error
  )
  {
    return opus_decoder_create(sampling_frequency, channels, error);
  }

  UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API OpusDecode(
    OpusDecoder* decoder,
    const unsigned char* data,
    opus_int32 len,
    opus_int16* pcm,
    int frame_size,
    int decode_fec
  )
  {
    return opus_decode(decoder, data, len, pcm, frame_size, decode_fec);
  }

  UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API OpusDecodeFloat(
    OpusDecoder* decoder,
    const unsigned char* data,
    opus_int32 len,
    float* pcm,
    int frame_size,
    int decode_fec
  )
  {
    return opus_decode_float(decoder, data, len, pcm, frame_size, decode_fec);
  }

  UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API OpusDecoderDestroy(
    OpusDecoder* decoder
  )
  {
    opus_decoder_destroy(decoder);
  }

  UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API OpusPcmSoftClip(
    float* pcm,
    int frame_size,
    int channels,
    float* softclipMem
  )
  {
    opus_pcm_soft_clip(pcm, frame_size, channels, softclipMem);
  }
}
