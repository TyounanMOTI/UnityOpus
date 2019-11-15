#include "opus.h"
#include "opus_defines.h"

#ifdef __cplusplus
extern "C" {
#endif

OpusEncoder *OpusEncoderCreate(opus_int32 samplingFrequency,int channels,int application,int *error){
    return opus_encoder_create(samplingFrequency, channels, application, error);
}

int OpusEncode(OpusEncoder *encoder,const opus_int16 *pcm,int frameSize,unsigned char *data,opus_int32 maxDataBytes){
    return opus_encode(encoder, pcm, frameSize, data, maxDataBytes);
}

int OpusEncodeFloat(OpusEncoder *encoder,const float *pcm,int frameSize,unsigned char *data,opus_int32 maxDataBytes){
    return opus_encode_float(encoder, pcm, frameSize, data, maxDataBytes);
}

void OpusEncoderDestroy(OpusEncoder *encoder){
    opus_encoder_destroy(encoder);
}

int OpusEncoderSetBitrate(OpusEncoder *encoder,int bitrate){
    return opus_encoder_ctl(encoder, OPUS_SET_BITRATE(bitrate));
}

int OpusEncoderSetComplexity(OpusEncoder *encoder,int complexity){
    return opus_encoder_ctl(encoder, OPUS_SET_COMPLEXITY(complexity));
}

int OpusEncoderSetSignal(OpusEncoder *encoder,int signal){
    return opus_encoder_ctl(encoder, OPUS_SET_SIGNAL(signal));
}

OpusDecoder *OpusDecoderCreate(opus_int32 samplingFrequency,int channels,int *error){
    return opus_decoder_create(samplingFrequency, channels, error);
}

int OpusDecode(OpusDecoder *decoder,const unsigned char *data,int len,opus_int16 *pcm,int frameSize,int decodeFec){
    return opus_decode(decoder, data, len, pcm, frameSize, decodeFec);
}

int OpusDecodeFloat(OpusDecoder *decoder,const unsigned char *data,int len,float *pcm,int frameSize,int decodeFec){
    return opus_decode_float(decoder, data, len, pcm, frameSize, decodeFec);
}

void OpusDecoderDestroy(OpusDecoder *decoder){
    opus_decoder_destroy(decoder);
}

void OpusPcmSoftClip(float *pcm,int frameSize,int channels,float *softclipMem){
    opus_pcm_soft_clip(pcm, frameSize, channels, softclipMem);
}

#ifdef __cplusplus
}
#endif
