function [ pCore, pDelta, bImage, bgmask ] = core_detection(filename, BN)
  
    image = imread(filename);
    if size(image,3) > 1 % if image is not 8 bit
        image = rgb2gray(image);
    end % convert image into grayscale
    image = double(image);

    %% Step 1: Gaussian filter + Segmentation
    blksize = 8;
    segThreshold = 0.25;
    [ bgmask ] = segmentation(image, blksize, segThreshold);

    % Downsize the original image
    bImage = imresize(image, 1/BN);
    %bImage = image;
    bgmask = imresize(bgmask, 1/BN);
    
    %% Step 2: compute Orientation Field
    [Theta, gSquSqu] = orientation_field(bImage, 15, 15);

    %% Step 3a: Poincare Index
    curveLen = 2;
    [pCore, pDelta] = poincare_index(Theta, bgmask, curveLen);
end