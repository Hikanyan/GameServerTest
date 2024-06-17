# ベースイメージとしてUnityの公式イメージを使用
FROM unityci/editor:ubuntu-2022.326f1

# 必要なパッケージをインストール
RUN apt-get update && apt-get install -y \
    curl \
    unzip

# プロジェクトのディレクトリを作成
WORKDIR /project

# PhotonとPlayFabのSDKをプロジェクトに追加
COPY ./Assets /project/Assets
COPY ./ProjectSettings /project/ProjectSettings

# ビルドスクリプトのコピー
COPY ./build.sh /project/build.sh

# ビルドスクリプトを実行
RUN chmod +x /project/build.sh
RUN /project/build.sh

# ポートの公開
EXPOSE 5055
EXPOSE 5056

# コンテナのエントリーポイント
ENTRYPOINT ["/project/Server.x86_64"]
