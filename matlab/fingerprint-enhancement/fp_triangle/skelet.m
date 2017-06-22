function [skeleton] =  skelet(im)
    
    if nargin == 0
        im = imread('images/1_3.bmp');
    end
    im = im(:,:,1);

    % Identify ridge-like regions and normalise image
    blksze = 16; thresh = 0.1;
    [normim, mask] = ridgesegment(im, blksze, thresh);
    
    % Determine ridge orientations
    [orientim, reliability] = ridgeorient(normim, 1, 5, 5);
    
    % Determine ridge frequency values across the image
    blksze = 36; 
    [freq, medfreq] = ridgefreq(normim, mask, orientim, blksze, 5, 5, 15);
    
    % Actually I find the median frequency value used across the whole
    % fingerprint gives a more satisfactory result...
    freq = medfreq.*mask;
    
    % Now apply filters to enhance the ridge pattern
    newim = ridgefilter(normim, orientim, freq, 0.5, 0.5, 1);
    
    % Binarise, ridge/valley threshold is 0
    binim = newim > 0;

    skeleton = fp_bwskel(binim);    
end