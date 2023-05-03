import * as jspb from 'google-protobuf'



export class MyMessage extends jspb.Message {
  getName(): string;
  setName(value: string): MyMessage;

  getMessage(): string;
  setMessage(value: string): MyMessage;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): MyMessage.AsObject;
  static toObject(includeInstance: boolean, msg: MyMessage): MyMessage.AsObject;
  static serializeBinaryToWriter(message: MyMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): MyMessage;
  static deserializeBinaryFromReader(message: MyMessage, reader: jspb.BinaryReader): MyMessage;
}

export namespace MyMessage {
  export type AsObject = {
    name: string,
    message: string,
  }
}

