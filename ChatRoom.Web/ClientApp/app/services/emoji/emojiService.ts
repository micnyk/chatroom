import { Injectable } from "@angular/core";
import { ChatMessage } from "../chat/chatMessage";

@Injectable()
export class EmojiService {
    parseMessage(message: ChatMessage): ChatMessage {
        this.emojiDictionary.forEach(emoji => {
            message.message = message.message.replace(emoji.pattern, this.getEmojiItem(emoji.emojiClass));
        });

        return message;
    }

    private emojiDictionary = [
        { pattern: /\:\)/g, emojiClass: "em-smiley" },
        { pattern: /\:\D/g, emojiClass: "em-grinning" },
        { pattern: /\;\(/g, emojiClass: "em-cry" },
        { pattern: /\:\(/g, emojiClass: "em-disappointed" },
        { pattern: /\:\*/g, emojiClass: "em-kissing" },
        { pattern: /xD/g, emojiClass: "em-pig" }
    ];

    private getEmojiItem(emojiClass: string): string {
        return `<i class='em ${emojiClass}'></i>`;
    }
}