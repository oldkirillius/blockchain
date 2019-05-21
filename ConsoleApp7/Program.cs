using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
namespace ConsoleApp7
{
    class Program
    {
        //Структура блока
        //  Первый шаг — определить элементы, которые должен содержать блок.
        //    Для простоты включим только самое необходимое: индекс(index),
        //     временную метку(timestamp), данные(data), хеш и хеш предыдущего блока, 
        //    который нужно записывать, чтобы сохранить структурную целостность цепи.

       public class Block {
            int index;
            string previousHash;
            string timestamp;
            string data;
            string hash;
            Block(int index, string previousHash, string timestamp, string data, string hash) {
                this.index = index;
                this.previousHash = previousHash;
                this.timestamp = timestamp;
                this.data = data;
                this.hash = hash;
            }


        }


        static void Main(string[] args)
        {

            //Хеш блока
            //    Хеширование блоков нужно 
            //    для сохранения целостности данных.В
            //    нашем примере для этого применяется алгоритм SHA-256.Этот вид хеша не
            //    имеет отношения к майнингу, так как мы в данном случае не реализуем
            //    защиту с помощью доказательства выполнения работы.

            string calculateHash(int index, string previousHash, string timestamp, string data, string hash)
            {
                string var1 = index + previousHash + timestamp + data + hash;
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(var1));

                    // Convert byte array to a string   
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }

            }
//            Генерируем блок
//Для генерации блока нам нужно знать хеш предыдущего
//                блока и внести остальные элементы, которые 
//                мы обозначили в структуре блока. Данные
//                предоставляются конечным пользователем.

            string generateNextBlock(strign blockData) {
                string previousBlock = getLatestBlock();
                int nextIndex = previousBlock.index + 1;
                int nextTimestamp = new Date().getTime() / 1000;
                string nextHash = calculateHash(nextIndex, previousBlock.hash, nextTimestamp, blockData);
                return new Block(nextIndex, previousBlock.hash, nextTimestamp, blockData, nextHash);
            }

            void getGenesisBlock() {

                return new Block(0, "0", 1465154705, "my genesis block!!", "816534932c2b7154836da6afc367695e6337db8a921823784c14378abed4f7d7");
            }

           string[] blockchain = getGenesisBlock();

            bool isValidNewBlock(Block newblock, Block previousBlock) {
                if (previousBlock.index + 1 !== newBlock.index)
                {
                    Console.WriteLine("неверный индекс");
                    return false;
                }
                else if (previousBlock.hash !== newBlock.previousHash)
                {
                    Console.WriteLine("неверный хеш предыдущего блока");
                    return false;
                }
                else if (calculateHashForBlock(newBlock) !== newBlock.hash)
                {
                    Console.WriteLine("неверный хеш: " + calculateHashForBlock(newBlock) + ' ' + newBlock.hash);
                    return false;
                }
                return true;


            }

           bool replaceChain(Block newBlock) {
                if (isValidChain(newBlocks) && newBlocks.length > blockchain.length)
                {
                    Console.WriteLine("Принятый блокчейн является валидным. Происходит замена текущего блокчейна на принятый");
                    blockchain = newBlocks;
                    broadcast(responseLatestMsg());
                }
                else
                {
                    Console.WriteLine("Принятый блокчейн не является валидны");
                }

            }



        }
    }
}
