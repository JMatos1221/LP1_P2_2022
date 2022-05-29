# Projeto 2 LP1 - Snakes & Ladders

## Autoria

### João Borges - a21904214
> Responsável por gerar o tabuleiro e *core loop* do jogo, e pelas ações especiais do tabuleiro (Snakes, Ladders, etc..).

> Responsável pela ação do *extra* e *cheat die*, prevenção de *loops* infinitos no jogo, documentação (comentários) e explicação das regras.

### João Matos - a21901219
> Responsável pela estruturação do projeto, representação do menu e tabela do jogo, movimento do jogador e *game loop*.

> Responsável pela implementação da interface IView, input *extra* e *cheat die* e implementação do *save* do jogo.

### Rodrigo Marques - a21802593
> Responsável pela estrutura dos dados do jogo, representação dos jogadores e descrição das casas do jogo, restrição da posição do jogador no tabuleiro e colisão.

> Responsável pela representação do *extra* e *cheat die*, utilização dos mesmos e sistema de *load*.

## Arquitetura da solução
Para a solução do nosso projeto, utilizamos o *design pattern* `MVC`, de forma a manter o projeto organizado em dados, lógica e visualização separados, não deixando o funcionamento lógico do projeto interferir com a sua representação e vice-versa.

Para os dados foram utilizados uma enumeração `Space`, que define os diferentes tipos de casa no tabuleiro, uma classe `Player` que guarda a aparência do jogador e a sua posição, utilizando propriedades para aceder às mesmas, e uma classe `Table` que vai conter um *array* bidimensional do tipo `Space`, para guardar o tabuleiro criado no início do jogo.

Além desta enumeração e classes responsáveis por guardar os dados do jogo, temos também uma classe `MainController` que serve de controlador, que contém toda a lógica do jogo, e uma classe `MainView` que implementa a interface `IView`, que se encarrega de representar todos os dados na consola e ler o *input* do utilizador.

Quando corremos o programa, os dados do tabuleiro (`Table`) são criados recebendo o seu tamanho. Quando criado o controlador `MainController`, recebe a `Table` já criada, correndo depois o método `GameLoop()` que dá início ao *core loop* do jogo e recebendo como parâmetro a `IView` previamente criada.

No *core loop*, temos um *loop* para as opções do menu onde o jogador deve escolher a opção que deseja: Começar um jogo novo, continuar um jogo salvo, ler as regras ou sair.

Começando um jogo novo, geramos o tabuleiro ao preencher o objeto `Table` (previamente criado e guardado no `MainController`) com casas normais, e ao gerar casas especiais com as suas devidas regras de quantidade mínima, quantidade máxima, fila mínima e fila máxima.  
Se selecionada a opção de continuar um jogo salvo, podemos também carregar o jogo através de um ficheiro guardado anteriormente `game.save`, caso este ficheiro não exista, é gerado um jogo novo.

Após termos o tabuleiro do jogo, entramos no *game loop*, onde nos é apresentado o tabuleiro ao início de cada jogada, e o jogador deve dar *input* para gerar um número aleatório que simula um dado, que depois fará o jogador avançar esse número de casas, ao ajustar a sua posição vertical e horizontal, garantindo que se encontra dentro dos limites do tabuleiro e aplicando a respetiva ação, ou ações em cadeia, caso seja o caso.
Inserindo `extra` o jogador pode utilizar o *extra die* caso o possua.  
O jogador pode inserir `save` para guardar o estado atual do jogo, sempre que for possível dar *input* para rolar o dado (caso esteja a ser questionado se quer utilizar o *cheat* die, não é possível).  

Após o movimento do jogador, verificamos se chegou ao fim do tabuleiro. Se sim, ele ganha o jogo, se não (e se não tiver o *cheat* die), o seu turno acaba e muda para o outro jogador (que deve agora dar *input*, repetindo este ciclo).
Caso o jogador tenha o *cheat* die, ele é questionado se o deseja usar (para o fazer basta inserir o novo valor do dado), caso não queira, ele pode simplesmente pressionar enter para prosseguir a jogada normalmente.  

## Diagrama UML Simples  
![Diagrama UML](images/UML.png)

## Referências
- Aulas de Linguagens de Programação 1 (slides).